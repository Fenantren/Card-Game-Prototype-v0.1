using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/DeckInfo")]
public class DeckInfoData : ScriptableObject
{
    [field: SerializeField] public List<CardData> DeckInfo {  get; private set; }
}
