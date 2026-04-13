using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] TMP_Text mana;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] SpriteRenderer imageSR;
    [SerializeField] GameObject wrapper;

    [SerializeField] LayerMask dropAreaLayer;

    private Vector3 dragStartPos;
    private Quaternion dragStartRot;
    

    public Card Card { get; private set; }
    public void Setup(Card card)
    {
        Card = card;
        mana.text = card.Mana.ToString();
        title.text = card.Title;
        description.text = card.Description;
        imageSR.sprite = card.Image;   
    }

    private void OnMouseEnter()
    {
        if (!Interactions.Instance.playerCanHover()) return;
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }
    private void OnMouseExit()
    {
        if (!Interactions.Instance.playerCanHover()) return;

        if (Interactions.Instance.IsTargeting == true) return;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
        

    }
    private void OnMouseDown()
    {
        if(!Interactions.Instance.playerCanInteract()) return;
        if( Card.ManualTargetEffect != null )
        {
            Interactions.Instance.IsTargeting = true;
            ManualTargetSystem.Instance.StartTargeting(transform.position);
            
            wrapper.SetActive(false);
            Vector3 pos = new(transform.position.x, -2, 0);
            
            CardViewHoverSystem.Instance.Show(Card, pos);

        }
        else
        {
            Interactions.Instance.playerIsDragging = true;
            wrapper.SetActive(true);
            CardViewHoverSystem.Instance.Hide();
            //Save the primary position of the card that is about to be dragged
            dragStartPos = transform.position;
            dragStartRot = transform.rotation;

            //FIX CARDS STAYING IN PLACE !
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        }

        
        
    }
    private void OnMouseDrag()
    {
        if(!Interactions.Instance.playerCanInteract()) return;
        if (Card.ManualTargetEffect != null) return;
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        
    }
    private void OnMouseUp()
    {
        if(!Interactions.Instance.playerCanInteract()) return;
        if(Card.ManualTargetEffect != null)
        {
            EnemyView target = ManualTargetSystem.Instance.EndTargeting(MouseUtil.GetMousePositionInWorldSpace(-1));
            Interactions.Instance.IsTargeting = false;


            if(target != null && ManaSystem.Instance.HasEnoughMana(Card.Mana))
            {
                PlayCardGA playCardGA = new(Card, target);
                ActionSystem.Instance.Perform(playCardGA);
                CardViewHoverSystem.Instance.Hide();
                wrapper.SetActive(true);
            }
            
            else
            {
                CardViewHoverSystem.Instance.Hide();
                wrapper.SetActive(true);
            }             
        }

        else
        {
            if (ManaSystem.Instance.HasEnoughMana(Card.Mana) && Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropAreaLayer))
            {
                PlayCardGA playCardGA = new(Card);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                transform.position = dragStartPos;
                transform.rotation = dragStartRot;

            }
            Interactions.Instance.playerIsDragging = false;
        }
        
    }
    
}
