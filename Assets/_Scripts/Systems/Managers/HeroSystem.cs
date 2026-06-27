using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView {  get;  set; }


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
