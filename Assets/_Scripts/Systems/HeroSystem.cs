using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView {  get;  set; }

    public void Setup(HeroData heroData)
    {
        HeroView.Setup(heroData);
    }

    public void RemoveShields()
    {
        HeroView.CurrentShield = 0;
        Debug.Log("HeroShield" + HeroView.CurrentShield.ToString());
        HeroView.UpdateShieldText();
    }
}
