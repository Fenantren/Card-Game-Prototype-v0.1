using System.Collections;
using UnityEngine;

public class HealSystem : MonoBehaviour
{
    [SerializeField] GameObject healVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<HealHeroGA>(HealHeroPerformer);
        ActionSystem.AttachPerformer<AddShieldGA>(AddShieldPerformer);

    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<HealHeroGA>();
        ActionSystem.DetachPerformer<AddShieldGA>();
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
    private IEnumerator AddShieldPerformer (AddShieldGA addShieldGA)
    {
        foreach (var target in addShieldGA.Targets)
        {
            target.AddShield(addShieldGA.ShieldAmount);
            //Add an VFX 
            Debug.Log("Shield added");
            yield return new WaitForSeconds(0.15f);
        }
    }
}
