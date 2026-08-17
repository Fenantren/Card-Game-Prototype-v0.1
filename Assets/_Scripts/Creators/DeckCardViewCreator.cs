using DG.Tweening;
using UnityEngine;

public class DeckCardViewCreator : Singleton<DeckCardViewCreator>
{
    [SerializeField] DeckCardView deckCardViewPrefab;
    
    public DeckCardView CreateDeckCardView(CardData card, Transform parent)
    {
        DeckCardView deckCardView = Instantiate(deckCardViewPrefab, parent);
        //deckCardView.transform.localScale = Vector3.zero;
        //deckCardView.transform.DOScale(Vector3.one * cardSizeScale, 0.15f);
        deckCardView.Setup(card);
        //deckCardView.OnClicked += DeckViewUISystem.Instance.ShowPreview;
        return deckCardView;
    }
}
