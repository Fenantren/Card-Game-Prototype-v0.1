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
        ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);

    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<AttackHeroGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
    }

    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
        Debug.Log("Enemy Setup"); 
    }

    public void UpdateEnemyAction(int turnNumber)
    {
        foreach ( var enemy in enemyBoardView.EnemyViews)
        {
            EnemyData data = enemy.EnemyData;
            enemy.AttackPower = data.EnemyActions[turnNumber - 1].AttackPower;
            enemy.AttackMultiplier = data.EnemyActions[turnNumber - 1].AttackMultiplier;
            enemy.UpdateAttackText();
        }
    }
    //Performers

    private IEnumerator EnemyTurnPerformer ( EnemyTurnGA enemyTurnGA)
    {
        //Cycle through all enemies and create AttackHeroGA for each enemy ,then add it to AS as reaction 
        foreach ( var enemy in enemyBoardView.EnemyViews )
        {
            AttackHeroGA attackHeroGA = new(enemy);
            ActionSystem.Instance.AddReaction(attackHeroGA);
        }
        yield return null;  
    }
    private IEnumerator AttackHeroPerformer ( AttackHeroGA attackHeroGA)
    {
        EnemyView attacker = attackHeroGA.Attacker;
        for (int i = 0; i < attacker.AttackMultiplier; i++)
        {
            //Animate attacker view 
            Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
            
            yield return tween.WaitForCompletion();
            
            
            //Pass deal damage ( variables : damage , list containing target/targets)
            DealDamageGA dealDamageGA = new(attacker.AttackPower, new() { HeroSystem.Instance.HeroView });
            //Add reaction to AS as SubFlow
            yield return ActionSystem.Instance.PerformSubFlow(dealDamageGA);  
            
        
            
            //Move the enemy back to primary position after attack is performed
            attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.15f);
            
            yield return new WaitForSeconds(0.15f);
            
            

        }
        
    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        //Call Remove Enemy IEnum
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
    }
}
