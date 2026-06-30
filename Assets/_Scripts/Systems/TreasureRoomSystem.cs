using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureRoomSystem : MonoBehaviour
{
    [SerializeField] GameObject collectButton;
    
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
