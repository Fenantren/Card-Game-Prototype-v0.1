using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
   public void OnClick()
    {
        if (!Interactions.Instance.playerCanInteract()) return;
        EnemyTurnGA enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
