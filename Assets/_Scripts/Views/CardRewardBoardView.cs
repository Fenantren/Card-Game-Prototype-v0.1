using System.Collections.Generic;
using UnityEngine;

public class CardRewardBoardView : MonoBehaviour
{
    [SerializeField] List<Transform> cardRewardSlots;

    public List<CardRewardView> CardRewardViews { get; private set; } = new();

    public void AddCardReward(Card card)
    {
        if (CardRewardViews.Count >= cardRewardSlots.Count)
        {
            Debug.LogWarning("No more reward slots available");
            return;
        }

        Transform cardRewardSlot = cardRewardSlots[CardRewardViews.Count];
        
        CardRewardView cardRewardView = CardRewardViewCreator.Instance.CreateCardRewardView(card, cardRewardSlot.position, cardRewardSlot.rotation);
        cardRewardView.transform.SetParent(cardRewardSlot, true);
        CardRewardViews.Add(cardRewardView);
    }

    
    public void ClearRewards()
    {
        foreach (var view in CardRewardViews)
        {
            Destroy(view.gameObject);
        }

        CardRewardViews.Clear();
    }
}
