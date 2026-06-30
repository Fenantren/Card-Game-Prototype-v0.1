using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSpecificSystem : MonoBehaviour
{
    
    public void ProceedToFinalScene()
    {
        MapSystem.Instance.CompleteNode();
        SceneManager.LoadScene(SceneNames.Final);
    }
}
