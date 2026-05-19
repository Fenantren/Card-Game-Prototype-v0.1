using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] TMP_Text attackText;
    
    public EnemyData EnemyData { get; private set; }
    public int AttackPower {  get;  set; } 
    public int AttackMultiplier { get;  set; }   
    
    public void Setup(EnemyData enemyData)
    {
        EnemyData = enemyData;
        AttackPower = enemyData.EnemyActions[0].AttackPower;
        AttackMultiplier = enemyData.EnemyActions[0].AttackMultiplier;
        UpdateAttackText();
        SetupBase(enemyData.Health, enemyData.Image, enemyData.Shield);
    }

    public void UpdateAttackText()
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
