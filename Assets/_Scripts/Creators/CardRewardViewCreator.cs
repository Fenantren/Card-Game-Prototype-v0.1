using DG.Tweening;
using UnityEngine;

public class CardRewardViewCreator : Singleton<CardRewardViewCreator>
{
    [SerializeField] CardRewardView cardRewardViewPrefab;
    [SerializeField] float cardSizeScale = 1f;

    public CardRewardView CreateCardRewardView ( Card card, Vector3 position, Quaternion rotation)
    {
        CardRewardView cardRewardView = Instantiate(cardRewardViewPrefab, position, rotation);
        cardRewardView.transform.localScale = Vector3.zero;
        cardRewardView.transform.DOScale(Vector3.one * cardSizeScale, 0.15f);
        cardRewardView.Setup(card);
        return cardRewardView;
    }
}
