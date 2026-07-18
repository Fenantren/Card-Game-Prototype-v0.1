using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] HeroView heroView;

    [SerializeField] HeroData heroData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HeroSystem.Instance.SetHeroView(heroView);

        HeroSystem.Instance.Setup(heroData);
    }

    
}
