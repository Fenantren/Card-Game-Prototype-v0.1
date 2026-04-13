using System.Collections;
using UnityEngine;

public class HealSystem : MonoBehaviour
{
    [SerializeField] GameObject healVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<HealHeroGA>(HealHeroPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<HealHeroGA>();
    }

    private IEnumerator HealHeroPerformer(HealHeroGA healHeroGA)
    {
        foreach(var target in healHeroGA.Targets) 
            {
                target.HealHealth(healHeroGA.HealAmount);
                Instantiate(healVFX, target.transform.position + Vector3.back * 2, healVFX.transform.rotation);
                Debug.Log("Hero healed");
                yield return new WaitForSeconds(0.15f);

            }
    }
}
