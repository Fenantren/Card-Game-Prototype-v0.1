using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : Singleton<ActionSystem>
{
    private List<GameAction> reactions = null;
    public bool isPerforming { get; private set; } = false;
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();
    private static Dictionary<Type, Func<GameAction, IEnumerator>> performers = new();
    private static Dictionary<ReactionTiming, Dictionary<Type, Dictionary<Delegate, Action<GameAction>>>> wrapperLookup = new();

    //Runs once automatically before any static method is used 
    static ActionSystem()
    {
        foreach (ReactionTiming timing in Enum.GetValues(typeof(ReactionTiming)))
        {
            wrapperLookup[timing] = new Dictionary<Type, Dictionary<Delegate, Action<GameAction>>>();
        }
    }


    public void Perform(GameAction action, System.Action OnPerformFinished = null)
    {
        if (isPerforming) return;
        isPerforming = true;
        StartCoroutine(Flow(action, () =>
        {
            isPerforming = false;
            OnPerformFinished?.Invoke();
        }));
    }

    public void AddReaction(GameAction gameAction)
    {
        reactions?.Add(gameAction);
    }

    private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
    {
        reactions = action.PreReactions;
        PerformSubscribers(action, preSubs);
        yield return PerformReactions();

        reactions = action.PerformReactions;
        yield return PerformPerformer(action);
        yield return PerformReactions();

        reactions = action.PostReactions;
        PerformSubscribers(action, postSubs);
        yield return PerformReactions();

        OnFlowFinished?.Invoke();

    }

    public IEnumerator PerformSubFlow(GameAction action)
    {
        yield return Flow(action);
    }

    private IEnumerator PerformPerformer( GameAction action)
    {
        Type type = action.GetType();
        if (performers.ContainsKey(type))
        {
            yield return performers[type](action);
        } 
    }
    private void PerformSubscribers( GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach ( var sub in subs[type])
            {
                sub(action);
            }
        }
    }

    private IEnumerator PerformReactions()
    {
        foreach ( var reaction in reactions)
        {
            yield return Flow(reaction);
        }
    }

    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
    {
        Type type = typeof(T);
        IEnumerator wrappedPerformer(GameAction action) => performer((T)action);
        if (performers.ContainsKey(type)) performers[type] = wrappedPerformer;
        else performers.Add(type, wrappedPerformer);
    }

    public static void DetachPerformer<T>() where T : GameAction
    {
        Type type = typeof(T);
        if (performers.ContainsKey(type)) performers.Remove(type);
    }

    public static void SubscribeReaction<T>(Action<T> reaction,ReactionTiming timing) where T : GameAction
    {
        var subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        Type type = typeof(T);

        // Ensure subscriber list exists
        if (!subs.TryGetValue(type, out var list))
        {
            list = new List<Action<GameAction>>();
            subs[type] = list;
        }

        // Create wrapper ONCE
        Action<GameAction> wrappedReaction = action => reaction((T)action);
        list.Add(wrappedReaction);

        // Store wrapper for later removal
        if (!wrapperLookup[timing].TryGetValue(type, out var map))
        {
            map = new Dictionary<Delegate, Action<GameAction>>();
            wrapperLookup[timing][type] = map;
        }

        map[reaction] = wrappedReaction;
    }


    public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing ) where T : GameAction
    {
        var subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        Type type = typeof(T);

        if (!subs.TryGetValue(type, out var list))
            return;

        if (!wrapperLookup[timing].TryGetValue(type, out var map))
            return;

        if (!map.TryGetValue(reaction, out var wrappedReaction))
            return;

        list.Remove(wrappedReaction);
        map.Remove(reaction);
    }
}
