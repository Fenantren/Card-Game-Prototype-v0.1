using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class RestSiteSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject proceedButton;
    [SerializeField] GameObject healButton;
    
    [Header("VFX")]
    [SerializeField] GameObject healVFX;
    
    [SerializeField] Vector3 healVFXOffset;

    [SerializeField] Transform heroViewPos;

    private void Start()
    { 
        HeroSystem.Instance.SpawnHeroView(heroViewPos);

        HeroSystem.Instance.Setup(HeroSystem.Instance.HeroData);
    }

    // TEMP - For TESTING ONLY ,remove once the Rest Scene finished 
    public void HalfHealth()
    {
        var heroView = FindFirstObjectByType<HeroView>();

        int maxHealth = heroView.MaxHealth;
        int halfHealth = (int)(0.5f * maxHealth);

        heroView.TakeDamage(halfHealth);

    }

    public void HealAtRestSite()
    {
        
        int maxHealth = HeroSystem.Instance.HeroView.MaxHealth;

        int amountToHeal = (int)(0.25f * maxHealth);
        HeroSystem.Instance.HeroView.HealHealth(amountToHeal);
        Instantiate(healVFX, heroViewPos.position + healVFXOffset , Quaternion.identity);

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
