using UnityEngine;
using UnityEngine.SceneManagement;

public class RestSiteSystem : MonoBehaviour
{
    [SerializeField] GameObject proceedButton;
    [SerializeField] GameObject healButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealAtRestSite()
    {
        // Heal for 25% max HP method 
        Debug.Log("Player Healed");
        healButton.SetActive(false);
        proceedButton.SetActive(true);
    }

    public void ProceedToLobby()
    {
        MapSystem.Instance.CompleteNode();
        SceneManager.LoadScene(SceneNames.Lobby);
    }
}
