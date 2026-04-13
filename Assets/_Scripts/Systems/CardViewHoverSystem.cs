using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>   
{
    [SerializeField] private CardView cardViewHover;
    

    public void Show(Card card, Vector3 position)
    {
        //check if another card is hovering already
        

        cardViewHover.gameObject.SetActive(true);
        cardViewHover.Setup(card);
        cardViewHover.transform.position = position;
    }
    public void Hide()
    {
        

        
        cardViewHover.gameObject.SetActive(false);
    }

    
}
