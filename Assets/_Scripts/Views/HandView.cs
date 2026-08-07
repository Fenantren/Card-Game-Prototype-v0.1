using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] SplineContainer splineContainer;
    private readonly List<CardView> cards = new();

    [SerializeField] float cardMoveSpeed = 0.25f;


    public CardView RemoveCard (Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null) return null;
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(cardMoveSpeed));
        return cardView;
    }

    private CardView GetCardView (Card card)
    {
        return cards.Where(cardView => cardView.Card == card).FirstOrDefault(); 
    }
    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPositions(cardMoveSpeed);
    }
    private IEnumerator UpdateCardPositions( float duration)
    {
        if (cards.Count == 0) yield break;
        //Adjust if cards should be spaced differently
        float cardSpacing = 1f / 7f;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        
        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles, duration);

        }
        yield return new WaitForSeconds(duration);
    }
}
