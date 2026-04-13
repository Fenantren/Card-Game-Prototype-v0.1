using UnityEngine;
using System.Collections.Generic;

public class HealHeroGA : GameAction
{
    public List<CombatantView> Targets { get; set; }
    public int HealAmount {  get; set; }
    
    public HealHeroGA (int healAmount, List<CombatantView> targets)
    { 
        HealAmount = healAmount;
        Targets = new(targets);
    }
}
