using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "Data/MapNode")]
public class MapNodeData : ScriptableObject
{
    [field: SerializeField] public RoomType RoomType { get; private set; }

    [field: SerializeField] public Sprite NodeSprite { get; private set; }

    [field: SerializeField] public List<EnemyData> Enemies { get; private set; } = null;

    [field: SerializeField] public List<MapNodeData> ChildrenNodes { get; private set; }


    private void OnValidate()
    {
        bool isCombatRoom = RoomType == RoomType.RCOMBAT ||
                            RoomType == RoomType.ECOMBAT ||
                            RoomType == RoomType.BOSS;
        if (isCombatRoom && (Enemies == null || Enemies.Count == 0))
        {
            Debug.LogWarning($"{name}: combat node has no enemies assigned!");
        }
    }
}
