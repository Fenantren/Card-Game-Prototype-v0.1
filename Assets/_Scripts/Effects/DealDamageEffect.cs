using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;
    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        
        //Pass Damage and list of targets/target to DealDamageGA 
        DealDamageGA dealDamageGA = new(damageAmount, targets);
        return dealDamageGA;
    }
}
