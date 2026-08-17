using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardView : MonoBehaviour
{
    [SerializeField] TMP_Text mana;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] Image cardImage;
    



    private CardData cardData;
    //public Card Card { get; private set; }
    public void Setup(CardData card)
    {
        cardData = card;
        mana.text = card.Mana.ToString();
        title.text = card.name;
        description.text = card.Description;
        cardImage.sprite = card.Image;   
    }

    
}
