using UnityEngine;
using System.Collections.Generic;

public class HeroTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        CombatantView target = HeroSystem.Instance.HeroView;
        return new() { target };
    }   
}
