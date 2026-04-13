using UnityEngine;

public class Interactions : Singleton<Interactions>
{
    public bool playerIsDragging { get; set; } = false;
    public bool playerCanInteract()
    {
        if (!ActionSystem.Instance.isPerforming) return true;
        else return false;
    }

    public bool playerCanHover()
    {
        if (playerIsDragging) return false;
        if (IsTargeting) return false;
        return true;
    }
    
    public bool IsTargeting { get; set; } = false;
}
