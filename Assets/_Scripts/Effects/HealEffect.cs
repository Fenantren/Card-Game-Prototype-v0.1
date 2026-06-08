using System.Collections.Generic;
using UnityEngine;

public class HealEffect : Effect
{
    [SerializeField] private int healAmount;

    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        HealGA healGA = new(healAmount, targets);
        return healGA;
    }
}
