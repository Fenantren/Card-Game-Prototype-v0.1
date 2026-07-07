using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckSystem : Singleton<DeckSystem>
{
    [SerializeField] private List<CardData> deck = new();

    public IReadOnlyList<CardData> Deck => deck;

    [SerializeField] TMP_Text deckUIText;

    public event Action<int> OnDeckChanged;


    protected override void Awake()
    {
        base.Awake();
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        
    }   
    
    public void InitializeDeck(List<CardData> startingDeck)
    {
        deck.Clear();
        deck.AddRange(startingDeck);
        UpdateUIText();
        OnDeckChanged?.Invoke(deck.Count);
    }
    public void AddCard(CardData card)
    {
        deck.Add(card);
        UpdateUIText();
        OnDeckChanged?.Invoke(deck.Count);
    }
    public void RemoveCard(CardData card)
    { 
        deck.Remove(card);
        UpdateUIText();
        OnDeckChanged?.Invoke(deck.Count);
    }

    public void SetUIText (TMP_Text text)
    {
        deckUIText = text;
        UpdateUIText();
    }
    public void UpdateUIText()
    {
        if (deckUIText == null) 
            return;

        deckUIText.text = deck.Count.ToString();
    }

    
}
