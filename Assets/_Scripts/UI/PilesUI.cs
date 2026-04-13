using TMPro;
using UnityEngine;

public class PilesUI : MonoBehaviour
{
    [SerializeField] TMP_Text drawPileText;
    [SerializeField] TMP_Text discardPileText;

    public void UpdatePilesText(int drawPileCount , int discardPileCount)
    {
        drawPileText.text = drawPileCount.ToString();
        discardPileText.text = discardPileCount.ToString();
    }
}
