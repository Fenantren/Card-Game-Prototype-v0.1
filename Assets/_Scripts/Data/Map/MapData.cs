using UnityEngine;

[CreateAssetMenu (menuName = "Data/Map")]
public class MapData : ScriptableObject
{
    [field : SerializeField] public MapNodeData StartingNode {  get; private set; }

    [field : SerializeField] public int ActNumber { get; private set; }
    [field : SerializeField] public string ActName { get; private set; }


}
