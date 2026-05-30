using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckSystem : Singleton<DeckSystem>
{
    [SerializeField] private List<CardData> deck = new();

    public IReadOnlyList<CardData> Deck => deck;

    [SerializeField] TMP_Text deckUIText;


    public void InitializeDeck(List<CardData> startingDeck)
    {
        deck.Clear();
        deck.AddRange(startingDeck);
        UpdateUIText();
    }
    public void AddCard(CardData card)
    {
        deck.Add(card);
        UpdateUIText(); 
    }
    public void RemoveCard(CardData card)
    { 
        deck.Remove(card);
        UpdateUIText(); 
    }

    public void UpdateUIText()
    {
        deckUIText.text = deck.Count.ToString();
    }
}
