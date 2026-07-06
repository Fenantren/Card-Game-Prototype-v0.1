using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    public HeroView HeroView {  get;  set; }
    private int persistedHealth;

    protected override void Awake()
    {
        base.Awake();
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }


    public void Setup(HeroData heroData)
    {
        HeroView.Setup(heroData);
        if(persistedHealth == 0)
        {
            persistedHealth = heroData.Health;
        }
        HeroView.SetCurrentHealth(persistedHealth);

        
    }

    public void SetHeroView(HeroView heroView)
    {
        HeroView = heroView;
    }

    public void SaveHealth()
    {
        persistedHealth = HeroView.CurrentHealth;
    }

    public void RemoveShields()
    {
        HeroView.CurrentShield = 0;
        
        HeroView.UpdateShieldText();
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RemoveShields();
    }
}
