using UnityEngine;


public class HeroView : CombatantView
{
    public void Setup(HeroData heroData) 
    {
        SetupBase(heroData.Health, heroData.Image, heroData.Shield);
    }

    //Overrides to health changing methods

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        HeroSystem.Instance.NotifyHealthChanged();
    }

    public override void HealHealth(int healAmount)
    {
        base.HealHealth(healAmount);
        HeroSystem.Instance.NotifyHealthChanged();
    }

    public override void SetCurrentHealth(int health)
    {
        base.SetCurrentHealth(health);
        HeroSystem.Instance.NotifyHealthChanged();
    }
}
