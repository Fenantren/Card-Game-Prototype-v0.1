
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CardRewardSystem : MonoBehaviour
{
    [SerializeField] CardRewardBoardView cardRewardBoardView;
    [SerializeField] GameObject cardRewardBoardObject;
    [SerializeField] DeckInfoData deckInfoData;

    
    [SerializeField] GameObject proceedButton;


    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<AllEnemiesDefeatedGA>(AllEnemiesDefeatedPostReaction, ReactionTiming.POST);
    }
    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<AllEnemiesDefeatedGA>(AllEnemiesDefeatedPostReaction, ReactionTiming.POST);
    }

    

    public void ShowCardRewards()
    {
       
       
            Setup();
            cardRewardBoardObject.SetActive(true);
            
       
    }
    public void Setup()
    {

        cardRewardBoardView.ClearRewards();

        List<CardData> rewardPool = new(deckInfoData.DeckInfo);
        
        for (int i = 0; i < 3; i++)
        {
            //If no rewards available, leave the loop 
            if (rewardPool.Count == 0) break;

            //Create a random index from the reward pool
            int index = Random.Range(0, rewardPool.Count);
            // Choose the card for this index and attach it to the selected reward...
            CardData selectedCardData = rewardPool[index];
            //... and then remove it from the list, to ensure it is not chosen again 
            rewardPool.RemoveAt(index);

            //Logic for setting up the rewards in the reward view 
            Card card = new(selectedCardData);
            cardRewardBoardView.AddCardReward(card);

            CardRewardView view = cardRewardBoardView.CardRewardViews[^1];

            view.OnCardSelected += HandleCardSelected;
        }
    }

    private void HandleCardSelected(Card selectedCard)
    {
        DeckSystem.Instance.AddCard(selectedCard.Data);

        cardRewardBoardView.ClearRewards();
        cardRewardBoardObject.SetActive(false);
        proceedButton.SetActive(true);
    }

    private void AllEnemiesDefeatedPostReaction(AllEnemiesDefeatedGA allEnemiesDefeatedGA)
    {
        ShowCardRewards();
    }
}
