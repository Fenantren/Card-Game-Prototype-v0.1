using UnityEngine;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] ArrowView arrowView;
    [SerializeField] LayerMask targetLayerMask;

    //Called when Targeting Mode is started
    public void StartTargeting(Vector3 startPosition)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.SetupArrow(startPosition);
    }

    //Called when we end the Targeting Mode
    public EnemyView EndTargeting(Vector3 endPosition)
    {
        arrowView.gameObject.SetActive(false);
        
        if(Physics.Raycast(endPosition,Vector3.forward, out RaycastHit hit, 10f, targetLayerMask) && hit.collider != null && hit.transform.TryGetComponent(out EnemyView enemyView))
        {
            return enemyView;
        }
        return null;

    }
}
