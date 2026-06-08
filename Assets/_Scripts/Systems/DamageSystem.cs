using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageSystem : MonoBehaviour
{
    [SerializeField] GameObject damageVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {   // Loop through each target and deal damage to it/them 

        
        foreach ( var target in dealDamageGA.Targets)
        {
            target.TakeDamage(dealDamageGA.DamageAmount);
            //VFX upon taking damage
            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            Debug.Log(target.ToString() + "hit");
            yield return new WaitForSeconds(0.15f);
            //Check if health is smaller than or equals 0
            if(target.CurrentHealth <= 0)
            {
                //Check if target is an enemy
                if(target is EnemyView enemyView)
                {
                    // Create KillEnemyGA and add it as reaction 
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddReaction(killEnemyGA);
                }
                else
                {
                    //Possible game over logic ,restart etc.
                }
            }
        }
    }
}
