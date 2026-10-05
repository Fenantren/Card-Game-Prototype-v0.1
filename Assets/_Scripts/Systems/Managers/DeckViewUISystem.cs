using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void CloseDeck()
    {
        deckViewUICanvas.SetActive(false);
        isDeckViewOpen = false;
        ClearAllCardViews();
    }

    private void SetupDeckViewUI()
    {
        IReadOnlyList<CardData> currentDeck = DeckSystem.Instance.Deck;

        foreach (CardData card in currentDeck)
        {
            DeckCardViewCreator.Instance.CreateDeckCardView(card, viewportContent);
        }
    }

    private void ClearAllCardViews()
    {
        foreach(Transform card in viewportContent)
        {
            Destroy(card.gameObject);
        }
    }
    public void ShowPreview()
    {

    }


}
