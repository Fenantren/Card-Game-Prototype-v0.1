using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;

[CreateAssetMenu(menuName = "Data/EnemyAction")]
public class EnemyActionData : ScriptableObject
{
    [field: SerializeField]
    public string ActionName { get; private set; }

    [field: SerializeReference, SR]
    public List<Effect> Effects { get; private set; }


}
