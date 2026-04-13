using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TMP_Text shieldText;

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }

    public int CurrentShield { get; private set; }  

    protected void SetupBase(int health, Sprite image, int shield)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        CurrentShield = shield;
        UpdateHealthText();
        UpdateShieldText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP : " + CurrentHealth;
    }

    private void UpdateShieldText()
    {
        shieldText.text = "Shield :" + CurrentShield;
    }
    //Update HP after taking damage
    public void TakeDamage(int damageAmount)
    {
        int remainingDamage = 0;
        if (CurrentShield >= 0)
        {
            CurrentShield -= damageAmount;
            
            if (damageAmount > CurrentShield)
            {
                remainingDamage = damageAmount - CurrentShield;
                Debug.Log("RemainDMG : " + remainingDamage);
            }
            if (CurrentShield < 0)
            {
                
                CurrentShield = 0;
                
                
                CurrentHealth -= damageAmount + remainingDamage ;
                //Lock HP on zero health 
                if (CurrentHealth < 0)
                {
                    CurrentHealth = 0;
                }
                //Animation upon taking damage
                transform.DOShakePosition(0.2f, 0.5f);
                //Update Health UI 
                UpdateHealthText();
            }
            UpdateShieldText();
        }
        
    }

    public void HealHealth (int healAmount)
    {
        CurrentHealth += healAmount;
        UpdateHealthText();
    }
}
