using System;
using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] TMP_Text enemyText;
    
    public EnemyData EnemyData { get; private set; }
    public EnemyActionData CurrentAction { get; private set; }    
    
    public void Setup(EnemyData enemyData)
    {
        EnemyData = enemyData;
       
        SetupBase(enemyData.Health, enemyData.Image, enemyData.Shield);

        SetCurrentAction(enemyData.EnemyActions[0]);
    }

    public void SetCurrentAction(EnemyActionData action)
    {
        CurrentAction = action;

        UpdateIntentUI();
    }

    private void UpdateIntentUI()
    {
        enemyText.text = CurrentAction.ActionName;
    }
}
