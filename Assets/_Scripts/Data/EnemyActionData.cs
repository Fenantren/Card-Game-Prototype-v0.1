using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;

[CreateAssetMenu(menuName = "Data/EnemyAction")]
public class EnemyActionData : ScriptableObject
{
    [field: SerializeField] public int AttackPower { get; private set; }
    [field: SerializeField] public int AttackMultiplier { get; private set; } = 1;

    
    [field: SerializeField] public List<AutoTargetEffect> ActionEffects { get; private set; } 
}
