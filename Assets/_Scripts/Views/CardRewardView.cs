using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardRewardView : MonoBehaviour
{
    [SerializeField] TMP_Text mana;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] SpriteRenderer imageSR;
    [SerializeField] GameObject wrapper;
    


    public event Action<Card> OnCardSelected;
    public Card Card { get; private set; }
    public void Setup(Card card)
    {
        Card = card;
        mana.text = card.Mana.ToString();
        title.text = card.Title;
        description.text = card.Description;
        imageSR.sprite = card.Image;
    }

    private void OnMouseEnter()
    {
        wrapper.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    private void OnMouseExit() 
    {
        wrapper.transform.localScale = Vector3.one;
    }

    private void OnMouseDown()
    {

        OnCardSelected?.Invoke(Card);

    }
}
