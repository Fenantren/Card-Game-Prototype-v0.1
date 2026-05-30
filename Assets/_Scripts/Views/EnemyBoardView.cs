using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoardView : MonoBehaviour
{
    [SerializeField] List<Transform> slots;
    public List<EnemyView> EnemyViews { get; private set; } = new();

    [SerializeField] CardRewardSystem cardRewardSystem;

    public void AddEnemy(EnemyData enemyData)
    {
        Transform slot = slots[EnemyViews.Count];
        EnemyView enemyView = EnemyViewCreator.Instance.CreateEnemyView(enemyData, slot.position, slot.rotation);
        enemyView.transform.parent = slot;
        EnemyViews.Add(enemyView);
    }

    public IEnumerator RemoveEnemy(EnemyView enemyView)
    {
        //Remove enemy view from enemies views list 
        EnemyViews.Remove(enemyView);
        //Animate enemy when killed 
        Tween tween = enemyView.transform.DOScale(Vector3.zero, 0.25f);
        yield return tween.WaitForCompletion();
        //Destroy enemyView GO 
        Destroy(enemyView.gameObject);
        
        //Check if all enemies are defeated , if so , end the match and show card rewards

        if (EnemyViews.Count <= 0)
        {
            cardRewardSystem.ShowCardRewards();
        }
    }

    
}
