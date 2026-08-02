using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionSystem : MonoBehaviour
{
    [SerializeField] HeroData selectedHeroData;
    [SerializeField] Image heroDisplay;
    [SerializeField] TMP_Text heroName;
    [SerializeField] ChoiceSelectUI[] choiceOptions;


    private void Awake()
    {
        if(selectedHeroData == null)
        {
            selectedHeroData = choiceOptions[0].heroData;
            UpdateDisplay();
        }
    }
    public void SelectHero(HeroData data)
    {
        selectedHeroData = data;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        heroDisplay.sprite = selectedHeroData.FullSprite;
        heroName.text = selectedHeroData.Name;
    }

    public void StartNewRun()
    {
        HeroSystem.Instance.SetHeroData(selectedHeroData);
        StartCoroutine(StartNewRunRoutine());
    }

    IEnumerator StartNewRunRoutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
