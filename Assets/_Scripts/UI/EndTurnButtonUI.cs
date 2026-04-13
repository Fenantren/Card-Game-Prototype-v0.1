using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
   public void OnClick()
    {
        if (ActionSystem.Instance.isPerforming) return;
        EnemyTurnGA enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
