using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSystem : Singleton<MapSystem>
{
    [field : SerializeField] public MapNodeData CurrentNode { get; private set; }

    private HashSet<MapNodeData> visitedNodes = new();

    [field: SerializeField] public MapData MapData { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeMap()
    {
       visitedNodes.Clear();
       CurrentNode = MapData.StartingNode;
    }

    public void CompleteNode()
    {
        visitedNodes.Add(CurrentNode);

    }

    public void TravelToNode(MapNodeData chosenNode)
    {
       CurrentNode = chosenNode;
    }

    
}
