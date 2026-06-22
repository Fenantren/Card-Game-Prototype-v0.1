using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] HeroData heroData;
    [SerializeField] List<EnemyData> enemyDatas;
    [SerializeField] TMP_Text deckUIText;

    private void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        
        EnemySystem.Instance.Setup(enemyDatas);
        if(DeckSystem.Instance.Deck.Count == 0)
        {
            DeckSystem.Instance.InitializeDeck(heroData.Deck);
        }
        
        CardSystem.Instance.Setup(DeckSystem.Instance.Deck.ToList());
        RefillManaGA refillManaGA = new();

        ActionSystem.Instance.Perform(refillManaGA, () =>
        {
            DrawCardsGA drawCardsGA = new(5);
            ActionSystem.Instance.Perform(drawCardsGA);

        });

        DeckSystem.Instance.SetUIText(deckUIText);
        
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
