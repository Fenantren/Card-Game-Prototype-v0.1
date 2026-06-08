using System.Collections.Generic;

[System.Serializable]

public abstract class Effect  
{
    public TargetType TargetType;
    public abstract GameAction GetGameAction(List<CombatantView> targets);
}
