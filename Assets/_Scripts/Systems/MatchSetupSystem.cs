using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] HeroData heroData;
    [SerializeField] List<EnemyData> enemyDatas;

    private void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.Setup(heroData.Deck);
        RefillManaGA refillManaGA = new();

        ActionSystem.Instance.Perform(refillManaGA, () =>
        {
            DrawCardsGA drawCardsGA = new(5);
            ActionSystem.Instance.Perform(drawCardsGA);

        });
        
    }
}
