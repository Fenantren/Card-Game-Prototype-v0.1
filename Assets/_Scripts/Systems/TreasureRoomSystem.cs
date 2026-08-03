using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureRoomSystem : MonoBehaviour
{
    [SerializeField] GameObject collectButton;

    [SerializeField] Transform heroViewPos;

    private void Start()
    {
        HeroSystem.Instance.SpawnHeroView(heroViewPos);

        HeroSystem.Instance.Setup(HeroSystem.Instance.HeroData);
    }
    public void HideButton()
    {
        collectButton.SetActive(false);
    }

    public void ProceedToLobby()
    {
        MapSystem.Instance.CompleteNode();
        SceneManager.LoadScene(SceneNames.Lobby);
    }
}
