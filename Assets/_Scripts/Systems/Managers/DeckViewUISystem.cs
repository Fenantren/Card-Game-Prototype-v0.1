using System.Collections.Generic;
using UnityEngine;

public class DeckViewUISystem : Singleton<DeckViewUISystem>
{
    [SerializeField] GameObject deckViewUICanvas;

    [SerializeField] bool isDeckViewOpen = false;

    [SerializeField] Transform viewportContent;


    public void OpenDeck()
    {
        deckViewUICanvas.SetActive(true);
        isDeckViewOpen = true;
        SetupDeckViewUI();
    }
    
    
    private void SetupDeckViewUI()
    {
        IReadOnlyList< CardData > currentDeck = DeckSystem.Instance.Deck;
        
        foreach (CardData card in currentDeck)
        {
            DeckCardViewCreator.Instance.CreateDeckCardView(card, viewportContent);
        }
    }
    public void ShowPreview()
    {

    }
}
