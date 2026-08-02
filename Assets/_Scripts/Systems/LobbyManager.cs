using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] HeroView heroView;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HeroSystem.Instance.SetHeroView(heroView);

        HeroSystem.Instance.Setup(HeroSystem.Instance.HeroData);
    }

    
}
