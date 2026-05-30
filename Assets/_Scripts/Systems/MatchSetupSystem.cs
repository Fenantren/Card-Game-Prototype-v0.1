using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] HeroData heroData;
    [SerializeField] List<EnemyData> enemyDatas;

    private void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        DeckSystem.Instance.InitializeDeck(heroData.Deck);
        CardSystem.Instance.Setup(DeckSystem.Instance.Deck.ToList());
        RefillManaGA refillManaGA = new();

        ActionSystem.Instance.Perform(refillManaGA, () =>
        {
            DrawCardsGA drawCardsGA = new(5);
            ActionSystem.Instance.Perform(drawCardsGA);

        });
        
    }
}
