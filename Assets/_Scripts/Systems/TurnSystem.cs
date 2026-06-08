using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TurnSystem : Singleton<TurnSystem>
{
    [SerializeField] public int turnNumber;
    [SerializeField] TMP_Text endTurnText;
    [SerializeField] GameObject cardRewardView;

    private void OnEnable()
    {
        turnNumber = 1;
        UpdateTurnText();
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
    }
    private void OnDisable()
    {
        turnNumber = 0;
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
    }

    void UpdateTurnText()
    {
        endTurnText.text = "End Turn " + turnNumber.ToString();
    }


    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        EnemySystem.Instance.RemoveShields();
    }
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        turnNumber++;
        UpdateTurnText();
        EnemySystem.Instance.SelectEnemyActions();
        HeroSystem.Instance.RemoveShields();
    }

    
}
