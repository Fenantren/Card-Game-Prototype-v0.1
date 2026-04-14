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
        shieldText.text = CurrentShield.ToString();
    }
    //Update HP after taking damage
    public void TakeDamage(int damageAmount)
    {
        int damageToHealth = damageAmount;

        if (CurrentShield > 0)
        {
            int absorbed = Mathf.Min (CurrentShield, damageAmount);

            CurrentShield -= absorbed;
            damageToHealth -= absorbed;

            UpdateShieldText();

        }
        if (damageToHealth > 0)
        {
            CurrentHealth -= damageToHealth;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            UpdateHealthText();
        }
        
        
    }

    public void HealHealth (int healAmount)
    {
        CurrentHealth += healAmount;
        UpdateHealthText();
    }

    public void AddShield ( int shieldAmount)
    {
        CurrentShield += shieldAmount;
        UpdateShieldText();
    }
}
