using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] HandView handView;
    [SerializeField] Transform drawPilePoint;
    [SerializeField] Transform discardPilePoint;
    //UI
    [SerializeField] PilesUI pilesUI;

    


    private readonly List<Card> drawPile = new();
    
    private readonly List<Card> discardPile = new();
    
    private readonly List<Card> hand = new();

    

    public void CheckPilesOnClick()
    {
        Debug.Log("Draw Pile Count :" + drawPile.Count.ToString());
        Debug.Log("Discard Pile Count :" + discardPile.Count.ToString());
        Debug.Log("Hand holding :" + hand.Count.ToString());
        Debug.Log("Total cards :" + (drawPile.Count + discardPile.Count + hand.Count).ToString());

    }
    void OnEnable()
    {
        Debug.Log("Enabled");
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);

        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();

        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        Debug.Log("Disabled");
        
    }

    //Publics

    //Sets up a draw pile ,when draw pile is empty
    public void Setup(IEnumerable<CardData> deckData)
    {
        //Safety in case Setup() is called more then once ! 
        drawPile.Clear();
        discardPile.Clear();
        hand.Clear();
        
        //Create runtime card instances
        foreach (var cardData in deckData)
        {
            AddToDrawPile(new Card(cardData));
        }
        // Randomize deck order once at combat start
        drawPile.Shuffle();
        //Update UI
        RefreshPileUI();
    } 
    //Performers

    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        
        
        for (int i = 0; i < drawCardsGA.Amount; i++)
        {
            if (drawPile.Count == 0)
            {
                if (discardPile.Count == 0)
                    yield break;

                RefillDeck();
            }

            yield return DrawCard();
        }

        RefreshPileUI();

    }
    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach (var card in hand.ToList())
        {
            
            CardView cardView = handView.RemoveCard(card);
            yield return MoveCardToDiscard(cardView);
            RefreshPileUI();
        }
        hand.Clear();

        
    }

    private IEnumerator PlayCardPerformer (PlayCardGA playCardGA)
    {

        RemoveFromHand(playCardGA.Card);

        CardView cardView = handView.RemoveCard(playCardGA.Card);
        
        //Deduct mana cost of this card from total mana 
        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);
        
        //Check if there is a manual target effect and if there is one, pass ManualTargetEffect and ManualTarget
        if (playCardGA.Card.ManualTargetEffect != null)
        {
            PerformEffectGA peformEffectGA = new(playCardGA.Card.ManualTargetEffect, new() { playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(peformEffectGA);
        }
        //Perform Effects of the card with effects other then ManualTarget ones
        foreach( var effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
        // This was being done twice, hence line of code was removed ( it was creating dbl copy of a discarded card after playing it)
        //discardPile.Add(playCardGA.Card);
        yield return MoveCardToDiscard(cardView);

        RefreshPileUI();
        
    }

    // Reactions

    //Performed BEFORE enemy turn  Game Action 
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
        
        
    }

    //Performed AFTER enemy turn  Game Action
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
        
    }

    //Helpers

    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        
        if (card == null) yield break;
        
        AddToHand(card);
        
        RefreshPileUI();

        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);

        yield return handView.AddCard(cardView); 
    }

    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        
        discardPile.Clear();

        drawPile.Shuffle();

        RefreshPileUI();

    }

    //Animate scale of cards to 0 and animates the position of cards to the discard pile point 
    private IEnumerator MoveCardToDiscard(CardView cardView)
    {
        AddToDiscardPile(cardView.Card);

        cardView.transform.DOScale(Vector3.zero, 0.15f);

        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);

        yield return tween.WaitForCompletion();

        Destroy(cardView.gameObject);
    }


    private void AddToDrawPile(Card card)
    {
        drawPile.Add(card);
        
    }

    private void AddToDiscardPile(Card card)
    {
        discardPile.Add(card);
        
    }

    private void AddToHand(Card card)
    {
        hand.Add(card);
    }

    private void RemoveFromHand(Card card)
    {
        hand.Remove(card);
    }


    private void RefreshPileUI()
    {
        pilesUI.UpdatePilesText(drawPile.Count, discardPile.Count);
    }
    
}
