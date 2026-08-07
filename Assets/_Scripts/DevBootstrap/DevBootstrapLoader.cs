using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DevBootstrapLoader : MonoBehaviour
{
    [SerializeField] HeroSystem heroSystemPrefab;
    [SerializeField] DeckSystem deckSystemPrefab;
    [SerializeField] MapSystem mapSystemPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
#if UNITY_EDITOR

        Instantiate(heroSystemPrefab);
        Instantiate(deckSystemPrefab);
        Instantiate(mapSystemPrefab);

        string heroPath = EditorPrefs.GetString("DevBootstrap_HeroPath", "");
        HeroData heroData = AssetDatabase.LoadAssetAtPath<HeroData>(heroPath);

        HeroSystem.Instance.SetHeroData(heroData);

        string targetScene = EditorPrefs.GetString("DevBootstrap_TargetScene", SceneNames.Combat);
        SceneManager.LoadScene(targetScene);
#endif
    }

    
}
