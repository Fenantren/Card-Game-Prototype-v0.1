using System.Collections.Generic;
using UnityEngine;

public class AddShieldGA : GameAction
{
    public List<CombatantView> Targets { get; set; }
    public int ShieldAmount { get; set; }

    public AddShieldGA ( int shieldAmount, List<CombatantView> targets)
    {
        ShieldAmount = shieldAmount;
        Targets = new(targets);
    }
}
