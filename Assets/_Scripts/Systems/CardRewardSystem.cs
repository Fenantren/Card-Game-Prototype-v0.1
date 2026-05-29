using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardRewardSystem : MonoBehaviour
{
    [SerializeField] CardRewardBoardView cardRewardBoardView;
    [SerializeField] GameObject cardRewardBoardObject;
    [SerializeField] DeckInfoData deckInfoData;
    
    [SerializeField] CardData cardData;

    
    public void ShowCardRewards(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
            Debug.Log("Button pressed");

            Setup();
            cardRewardBoardObject.SetActive(true);

        

    }

    public void Setup()
    {

        cardRewardBoardView.ClearRewards();

        for ( int i = 0; i < 3; i++)
        {
            Card card = new(cardData);
            cardRewardBoardView.AddCardReward(card);
         
        }
    }

    
}
