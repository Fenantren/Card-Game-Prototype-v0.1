using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardRewardSystem : MonoBehaviour
{
    [SerializeField] GameObject cardRewardView;
    [SerializeField] DeckInfoData deckInfoData;
    [SerializeField] List<Transform> cardSlots;
    [SerializeField] CardData cardData;

    public void ShowCardRewards(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Button pressed");
            
            
            cardRewardView.SetActive(true);

        }

    }

    /*public void AddCard(CardData cardData)
    {
        Transform cardSlot = cardSlots[0];

    }*/
}
