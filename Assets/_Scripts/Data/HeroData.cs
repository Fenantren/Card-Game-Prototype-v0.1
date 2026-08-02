using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Hero")]

public class HeroData : ScriptableObject
{
    [field: SerializeField] public string Name {  get; private set; }
    [field: SerializeField] public Sprite HeadSprite { get; private set; }
    [field: SerializeField] public Sprite FullSprite {  get; private set; }

    [field: SerializeField] public int Health {  get; private set; }
    [field: SerializeField] public int Shield {  get; private set; }

    [field: SerializeField] public List<CardData> Deck {  get; private set; }
}
