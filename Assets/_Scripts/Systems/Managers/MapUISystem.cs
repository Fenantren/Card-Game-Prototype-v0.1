using System.Collections.Generic;
using UnityEngine;

public class MapUISystem : MonoBehaviour
{
    [SerializeField] MapVisualConfig config;
    [SerializeField] GameObject mapPanel;
    [SerializeField] Transform nodeContainer;
    [SerializeField] RectTransform linePrefab;
    [SerializeField] float floorSpacing;
    [SerializeField] float nodeSpacing;

    private Dictionary<int, List<MapNodeData>> nodesByFloor = new();
    private Dictionary<MapNodeData, MapNodeView> nodeViews = new();

    public void OpenMap()
    {
        mapPanel.SetActive(true);
    }

    public void CloseMap()
    {
        mapPanel.SetActive(false);
    }

    private void CollectNodesByFloor()
    {
        nodesByFloor.Clear();

        Queue<(MapNodeData node, int depth)> queue = new();

        queue.Enqueue((MapSystem.Instance.MapData.StartingNode, 0));

        HashSet<MapNodeData> visited = new();
        visited.Add(MapSystem.Instance.MapData.StartingNode);

        while (queue.Count > 0)
        {
            var (node, depth) = queue.Dequeue();

            if (!nodesByFloor.ContainsKey(depth))
            {
                nodesByFloor[depth] = new List<MapNodeData>();
            }
            nodesByFloor[depth].Add(node);

            //Add children to next depth(with a check if they havent been added )
            foreach (var child in node.ChildrenNodes)
            {
                if (!visited.Contains(child))
                {
                    visited.Add(child);
                    queue.Enqueue((child, depth + 1));
                }
            }

        }
    }
}
