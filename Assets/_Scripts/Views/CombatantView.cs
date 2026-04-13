using DG.Tweening;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] SpriteRenderer spriteRenderer;

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }

    protected void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP: " + CurrentHealth;
    }
    //Update HP after taking damage
    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        //Lock HP on zero health 
        if(CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        //Animation upon taking damage
        transform.DOShakePosition(0.2f, 0.5f);
        //Update Health UI 
        UpdateHealthText();
    }

    public void HealHealth (int healAmount)
    {
        CurrentHealth += healAmount;
        UpdateHealthText();
    }
}
