using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;



public class DevBoostrapWindow : EditorWindow

{
    [MenuItem("DevTools/BootstrapWindow")]
    public static void OpenWindow()
    {
        GetWindow<DevBoostrapWindow>("Dev Bootstrap");
    }

    // --- Fields ---
    private bool isEnabled;
    private HeroData selectedHero;
    private DeckInfoData selectedDeck;
    private int targetSceneIndex; // index into your hardcoded scene list
    
    private string[] sceneOptions =
    {
        SceneNames.Combat,
        SceneNames.Lobby,
        SceneNames.Rest,
        SceneNames.Treasure,
        SceneNames.Boss,
        SceneNames.Final
    };

    private int targetHeroIndex;
    private string[] heroNames = { "Crow", "Blue Crow", "Brown Crow" };
    private HeroData[] heroOptions;

    private int targetDeckIndex;
    private string[] deckNames = { "Main" };
    private DeckInfoData[] deckOptions;

    // --- OnEnable ---
    private void OnEnable()
    {
        // primitives load directly
        isEnabled = EditorPrefs.GetBool("DevBootstrap_Enabled", false);
        
        targetSceneIndex = EditorPrefs.GetInt("DevBootstrap_SceneIndex", 0);
        
        targetHeroIndex = EditorPrefs.GetInt("DevBootstrap_HeroIndex", 0);

        // ScriptableObjects load via their saved path
        heroOptions = new HeroData[]
        {
            AssetDatabase.LoadAssetAtPath<HeroData>("Assets/Data/Heroes/Crow.asset"),
            AssetDatabase.LoadAssetAtPath<HeroData>("Assets/Data/Heroes/BlueCrow.asset"),
            AssetDatabase.LoadAssetAtPath<HeroData>("Assets/Data/Heroes/BrownCrow.asset")
        };

        selectedHero = heroOptions[targetHeroIndex];

        targetDeckIndex = EditorPrefs.GetInt("DevBootstrap_DeckIndex", 0);


        deckOptions = new DeckInfoData[]
        {
            AssetDatabase.LoadAssetAtPath<DeckInfoData>("Assets/Data/Deck Info/Main.asset")
        };

        selectedDeck = deckOptions[targetDeckIndex];

        

        
    }

    private void SavePrefs()
    {
        //Save Enabled and SceneIndex
        EditorPrefs.SetBool("DevBootstrap_Enabled", isEnabled);
        
        EditorPrefs.SetInt("DevBootstrap_SceneIndex", targetSceneIndex);

        EditorPrefs.SetInt("DevBootstrap_HeroIndex", targetHeroIndex);

        EditorPrefs.SetInt("DevBootstrap_DeckIndex", targetDeckIndex);
        // ScriptableObjects: save asset path, empty string if unassigned
        string heroPath = selectedHero != null ? AssetDatabase.GetAssetPath(selectedHero) : "";
        EditorPrefs.SetString("DevBootstrap_HeroPath", heroPath);

        string deckPath = selectedDeck != null ? AssetDatabase.GetAssetPath(selectedDeck) : "";
        EditorPrefs.SetString("DevBootstrap_DeckPath", deckPath);


        if (isEnabled)
        {
            // point Unity to the bootstrap scene when Play is pressed
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/DevBootstrap.unity");
        }
        else
        {
            // restore normal behaviour
            EditorSceneManager.playModeStartScene = null;
        }
    }

    private void OnGUI()
    {
        isEnabled = EditorGUILayout.Toggle("Dev Mode", isEnabled);
        //Hero dropdown 
        targetHeroIndex = EditorGUILayout.Popup("Hero", targetHeroIndex, heroNames);
        selectedHero = heroOptions[targetHeroIndex];

        //Deck dropdown
        targetDeckIndex = EditorGUILayout.Popup("Deck", targetDeckIndex, deckNames);
        selectedDeck = deckOptions[targetDeckIndex];

        //Scene dropdown
        targetSceneIndex = EditorGUILayout.Popup("Scene", targetSceneIndex, sceneOptions);

        if (GUILayout.Button("Save"))
        {
            SavePrefs();
        }
    }
}
