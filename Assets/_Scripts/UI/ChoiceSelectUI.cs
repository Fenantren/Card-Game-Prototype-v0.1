using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSelectUI : MonoBehaviour
{
    [SerializeField] public HeroData heroData;
    [SerializeField] Image choiceHeadSprite;
    [SerializeField] TMP_Text choiceName;
    [SerializeField] CharacterSelectionSystem characterSelectionSystem;

    private void Awake()
    {
        characterSelectionSystem = FindFirstObjectByType<CharacterSelectionSystem>();
        choiceHeadSprite.sprite = heroData.HeadSprite;
        choiceName.text = heroData.Name;
    }

    public void OnChoiceClicked()
    {
        characterSelectionSystem.SelectHero(heroData);
    }

    
}
