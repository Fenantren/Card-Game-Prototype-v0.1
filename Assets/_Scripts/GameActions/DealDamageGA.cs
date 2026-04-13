using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction
{
    public int DamageAmount { get; set; }
    // Combatant view is used below ,so it is usable for both enemies and hero (plus funconality for attacking all enemies) 
    public List<CombatantView> Targets { get; set; }
    public DealDamageGA(int amount, List<CombatantView> targets)
    {
        //Pass over the damage amount and target/targets via List 
        DamageAmount = amount;  
        Targets = new (targets);
    }
}
    

