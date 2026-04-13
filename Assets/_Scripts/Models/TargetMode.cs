using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//Each implementation of this class will be responsible for providing the correct target
public abstract class TargetMode 
{
    public abstract List<CombatantView> GetTargets();
}
