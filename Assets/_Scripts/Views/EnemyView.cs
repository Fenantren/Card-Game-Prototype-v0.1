using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] TMP_Text attackText;
    
    public int AttackPower {  get; private set; } 
    public int AttackMultiplier { get; private set; }   
    
    public void Setup(EnemyData enemyData)
    {
        AttackPower = enemyData.EnemyActions[0].AttackPower;
        AttackMultiplier = enemyData.EnemyActions[0].AttackMultiplier;
        UpdateAttackText();
        SetupBase(enemyData.Health, enemyData.Image, enemyData.Shield);
    }

    private void UpdateAttackText()
    {
        if(AttackMultiplier == 1)
        {
            attackText.text = "ATK: " + AttackPower;

        }
        else
        {
            attackText.text = "ATK: " + AttackPower + " X " + AttackMultiplier;
        }
    }
}
