using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject shieldSprite;
    [SerializeField] TMP_Text shieldText;
    [SerializeField] HealthBar healthBarScript;

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; protected set; }

    public int CurrentShield { get;  set; }  

    protected void SetupBase(int health, Sprite image, int shield)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        CurrentShield = shield;
        UpdateHealthText();
        UpdateShieldText();
        healthBarScript.SetMaxHealth(health);
    }

    public void RefreshHealthDisplay()
    {
        UpdateHealthText();
        healthBarScript.SetHealth(CurrentHealth);
    }

    public virtual void SetCurrentHealth( int health)
    {
        CurrentHealth = Mathf.Clamp(health, 0, MaxHealth);
        RefreshHealthDisplay();
    }

    private void UpdateHealthText()
    {
        healthText.text = CurrentHealth + "/" + MaxHealth;
    }

    public void UpdateShieldText()
    {
        shieldText.text = CurrentShield.ToString();

        if (CurrentShield <= 0)
        {
            shieldText.enabled = false;
            shieldSprite.SetActive(false);
        }
        else
        
            shieldSprite.SetActive(true);
            shieldText.enabled = true;
    }
    //Update HP after taking damage
    public virtual void TakeDamage(int damageAmount)
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
            healthBarScript.SetHealth(CurrentHealth);
            transform.DOShakePosition(0.2f, 0.5f);
        }
        
        
    }

    public virtual void HealHealth (int healAmount)
    {
        if(CurrentHealth + healAmount > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {

            CurrentHealth += healAmount;
        }
        UpdateHealthText();
        healthBarScript.SetHealth(CurrentHealth);
    }

    public void AddShield ( int shieldAmount)
    {
        CurrentShield += shieldAmount;
        UpdateShieldText();
    }

    public void RemoveShields()
    {
        CurrentShield = 0;
        
       UpdateShieldText();
    }
}
