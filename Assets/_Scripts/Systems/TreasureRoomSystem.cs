using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureRoomSystem : MonoBehaviour
{
    [SerializeField] GameObject collectButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
