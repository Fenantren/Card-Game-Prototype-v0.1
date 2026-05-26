using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardRewardSystem : MonoBehaviour
{
    [SerializeField] GameObject cardRewardView;
    [SerializeField] DeckInfoData deckInfoData;

    public void Setup(List<DeckInfoData> deckInfoDatas)
    {
        
    }

    public void ShowCardRewards(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Button pressed");
            
            
            cardRewardView.SetActive(true);

        }

    }
}
