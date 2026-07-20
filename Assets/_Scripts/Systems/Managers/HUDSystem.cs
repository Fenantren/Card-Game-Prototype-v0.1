
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDSystem : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text deckText;
    [SerializeField] TMP_Text floorText;
    [SerializeField] TMP_Text actText;

    private void OnEnable()
    {
        if(HeroSystem.Instance != null) 
        HeroSystem.Instance.OnHealthChanged += UpdateHealthDisplay;
        
        if(DeckSystem.Instance != null)
        DeckSystem.Instance.OnDeckChanged += UpdateDeckDisplay;
    }

    private void OnDisable()
    {
        if(HeroSystem.Instance != null)
        HeroSystem.Instance.OnHealthChanged -= UpdateHealthDisplay;
        
        if(DeckSystem.Instance != null)
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
        if (HeroSystem.Instance.HeroView != null)
        {
            UpdateHealthDisplay(HeroSystem.Instance.HeroView.CurrentHealth, HeroSystem.Instance.HeroView.MaxHealth);
        }

        UpdateDeckDisplay(DeckSystem.Instance.Deck.Count);

        actText.text = MapSystem.Instance.MapData.ActName;

        floorText.text = SceneManager.GetActiveScene().name == SceneNames.Lobby ? "Lobby" :
            "Floor " + MapSystem.Instance.CurrentFloor.ToString();

    }

    
}
