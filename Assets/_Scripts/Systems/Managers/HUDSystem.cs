
using TMPro;
using UnityEngine;

public class HUDSystem : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text deckText;
    [SerializeField] TMP_Text floorText;

    private void OnEnable()
    {
        HeroSystem.Instance.OnHealthChanged += UpdateHealthDisplay;
        DeckSystem.Instance.OnDeckChanged += UpdateDeckDisplay;
    }

    private void OnDisable()
    {
        HeroSystem.Instance.OnHealthChanged -= UpdateHealthDisplay;
        DeckSystem.Instance.OnDeckChanged -= UpdateDeckDisplay;
    }

    private void UpdateDeckDisplay(int deckCount)
    {
        deckText.text = deckCount.ToString();
    }

    private void UpdateHealthDisplay(int current, int max)
    {
        healthText.text = current + "/" + max;
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentHealth = HeroSystem.Instance.HeroView.CurrentHealth;
        int maxHealth = HeroSystem.Instance.HeroView.MaxHealth;
        
        UpdateHealthDisplay(currentHealth, maxHealth);

        UpdateDeckDisplay(DeckSystem.Instance.Deck.Count);

        floorText.text = MapSystem.Instance.CurrentFloor.ToString();

    }

    
}
