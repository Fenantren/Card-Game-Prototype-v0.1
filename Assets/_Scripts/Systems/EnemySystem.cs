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
            Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.25f);
            
            yield return tween.WaitForCompletion();
            Debug.Log("Move forward complete");
            DealDamageGA dealDamageGA = new(attacker.AttackPower, new() { HeroSystem.Instance.HeroView });
        
            //Add reaction to AS
            //ActionSystem.Instance.AddReaction(dealDamageGA);
              yield return ActionSystem.Instance.PerformSubFlow(dealDamageGA);  
            Debug.Log("Enemy Attack");
        
            attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
            Debug.Log("Move backward complete");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Loop complete");
            //Pass deal damage ( variables : damage , list containing target/targets)
            

        }
        
    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        //Call Remove Enemy IEnum
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
    }
}
