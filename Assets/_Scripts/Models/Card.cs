using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    // These data will not be set ,just return the value from data
    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;
    public Effect ManualTargetEffect => data.ManualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => data.OtherEffects;    
    // These will be set
    public int Mana { get; private set; }
    

    private readonly CardData data;
    public Card(CardData cardData)
    {
        data = cardData;
        Mana = data.Mana;

    }
}
