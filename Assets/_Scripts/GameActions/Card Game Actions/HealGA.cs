using UnityEngine;
using System.Collections.Generic;

public class HealGA : GameAction
{
    public List<CombatantView> Targets { get; set; }
    public int HealAmount {  get; set; }
    
    public HealGA (int healAmount, List<CombatantView> targets)
    { 
        HealAmount = healAmount;
        Targets = new(targets);
    }
}
