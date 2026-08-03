using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] Transform heroViewPos;

    
    
    void Start()
    {
        HeroSystem.Instance.SpawnHeroView(heroViewPos);

        HeroSystem.Instance.Setup(HeroSystem.Instance.HeroData);
    }

    
}
