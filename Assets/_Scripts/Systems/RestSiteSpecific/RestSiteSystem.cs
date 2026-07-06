using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class RestSiteSystem : MonoBehaviour
{
    [SerializeField] GameObject proceedButton;
    [SerializeField] GameObject healButton;
    [SerializeField] GameObject healVFX;
    [SerializeField] Transform healPosition;
    [SerializeField] Vector3 healVFXOffset;
    [SerializeField] HeroData heroData;
    [SerializeField] HeroView heroView;

    private void Start()
    { 
        HeroSystem.Instance.SetHeroView(heroView);

        HeroSystem.Instance.Setup(heroData);
    }

    // TEMP - For TESTING ONLY ,remove once the Rest Scene finished 
    public void HalfHealth()
    {
        

        int maxHealth = heroView.MaxHealth;
        int halfHealth = (int)(0.5f * maxHealth);

        heroView.TakeDamage(halfHealth);

    }

    public void HealAtRestSite()
    {
        
        int maxHealth = HeroSystem.Instance.HeroView.MaxHealth;

        int amountToHeal = (int)(0.25f * maxHealth);
        HeroSystem.Instance.HeroView.HealHealth(amountToHeal);
        Instantiate(healVFX, healPosition.position + healVFXOffset , Quaternion.identity);

        healButton.SetActive(false);
        StartCoroutine(WaitForHealVFX());

        
    }

    public void ProceedToLobby()
    {
        MapSystem.Instance.CompleteNode();
        SceneManager.LoadScene(SceneNames.Lobby);
    }

    private IEnumerator WaitForHealVFX()
    {
       yield return new WaitForSeconds(2f);
        proceedButton.SetActive(true);
        
    }
}
