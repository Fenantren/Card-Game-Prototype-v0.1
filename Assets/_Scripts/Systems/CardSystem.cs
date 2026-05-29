using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] HandView handView;
    [SerializeField] Transform drawPilePoint;
    [SerializeField] Transform discardPilePoint;
    //UI
    [SerializeField] PilesUI pilesUI;

    


    private readonly List<Card> drawPile = new();
    
    private readonly List<Card> discardPile = new();
    
    private readonly List<Card> hand = new();

    

    public void CheckPilesOnClick()
    {
        Debug.Log("Draw Pile Count :" + drawPile.Count.ToString());
        Debug.Log("Discard Pile Count :" + discardPile.Count.ToString());
        Debug.Log("Hand holding :" + hand.Count.ToString());
        Debug.Log("Total cards :" + (drawPile.Count + discardPile.Count + hand.Count).ToString());

    }
    void OnEnable()
    {
        Debug.Log("Enabled");
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);

        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();

        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        Debug.Log("Disabled");
        
    }

    //Publics

    //Sets up a draw pile ,when draw pile is empty
    public void Setup(List<CardData> deckData)
    {
        pilesUI.UpdatePilesText(drawPile.Count, discardPile.Count);
        
        foreach (var cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }
    } 
    //Performers

    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        int actualAmount = Mathf.Min(drawCardsGA.Amount, drawPile.Count);
        int notDrawnAmount = drawCardsGA.Amount - actualAmount;
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }
        
        if ( notDrawnAmount > 0)
        {
            RefillDeck();
            
            for (int i = 0; i < notDrawnAmount; i++)
            {
                yield return DrawCard();
            }
        }
        pilesUI.UpdatePilesText(drawPile.Count, discardPile.Count);
    }
    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach (var card in hand)
        {
            
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
        pilesUI.UpdatePilesText(drawPile.Count, discardPile.Count);
    }

    private IEnumerator PlayCardPerformer (PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);
        CardView cardView = handView.RemoveCard(playCardGA.Card);
        
        //Deduct mana cost of this card from total mana 
        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);
        
        //Check if there is a manual target effect and if there is one, pass ManualTargetEffect and ManualTarget
        if (playCardGA.Card.ManualTargetEffect != null)
        {
            PerformEffectGA peformEffectGA = new(playCardGA.Card.ManualTargetEffect, new() { playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(peformEffectGA);
        }
        //Perform Effects of the card with effects other then ManualTarget ones
        foreach( var effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
        // This was being done twice, hence line of code was removed ( it was creating dbl copy of a discarded card after playing it)
        //discardPile.Add(playCardGA.Card);
        yield return DiscardCard(cardView);
        pilesUI.UpdatePilesText(drawPile.Count, discardPile.Count);
    }

    // Reactions

    //Performed BEFORE enemy turn  Game Action 
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
        
        
    }

    //Performed AFTER enemy turn  Game Action
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
        
    }

    //Helpers

    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        if (card == null) yield break;
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView); 
    }

    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();

    }

    //Animate scale of cards to 0 and animates the position of cards to the discard pile point 
    private IEnumerator DiscardCard(CardView cardView)
    {
        discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }

    
}
