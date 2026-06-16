using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] EnemyBoardView enemyBoardView;
    
    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);

        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);

        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        
        ActionSystem.DetachPerformer<KillEnemyGA>();

        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);

        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData); 
        }
        
    }

    public void SelectEnemyActions()
    {
        foreach (EnemyView enemy in Enemies)
        {
            EnemyActionData action = ChooseAction(enemy);

            enemy.SetCurrentAction(action);
        }
    }

    private EnemyActionData ChooseAction(EnemyView enemy)
    {
        int index = (TurnSystem.Instance.turnNumber - 1) % enemy.EnemyData.EnemyActions.Count;


        return enemy.EnemyData.EnemyActions[index];
    }
    //Performers

    private IEnumerator EnemyTurnPerformer ( EnemyTurnGA enemyTurnGA)
    {
         
        foreach ( EnemyView enemy in Enemies )
        {
            EnemyActionData action = enemy.CurrentAction;

            foreach (Effect effect in action.Effects)
            {
                List<CombatantView> targets = GetTargets(effect, enemy);

                GameAction gameAction = effect.GetGameAction(targets);

                yield return ActionSystem.Instance.PerformSubFlow(gameAction);
            }
        }
          
    }

    private List<CombatantView> GetTargets(Effect effect, EnemyView enemy)
    {
        switch(effect.TargetType)
        {
            case TargetType.Self:
                return new List<CombatantView>()
                {
                    enemy
                };
            
            case TargetType.Hero:
                return new List<CombatantView>()
                {
                    HeroSystem.Instance.HeroView
                };
            
            case TargetType.AllEnemies:
                return Enemies.ConvertAll<CombatantView>(enemyView => enemyView);
            
            default:
                return new List<CombatantView>();
        }
    }
   
    public void RemoveShields()
    {
        foreach (EnemyView enemy in Enemies)
        {
            enemy.RemoveShields();
            
        }
    }
    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        //Call Remove Enemy IEnum
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
    }

    
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        RemoveShields();
    }
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        
        SelectEnemyActions();
    }
}
