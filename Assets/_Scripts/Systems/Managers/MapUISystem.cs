using System.Collections.Generic;

using UnityEngine;

public class MapUISystem : MonoBehaviour
{
    [SerializeField] MapVisualConfig config;
    [SerializeField] GameObject mapPanel;
    [SerializeField] RectTransform nodeContainer;
    [SerializeField] RectTransform linePrefab;
    [SerializeField] float floorSpacing;
    [SerializeField] float nodeSpacing;
    [SerializeField] float linePadding;

    private Dictionary<int, List<MapNodeData>> nodesByFloor = new();
    private Dictionary<MapNodeData, MapNodeView> nodeViews = new();


    private void Start()
    {
        CollectNodesByFloor();
        SpawnNodes();
        DrawLines();
        mapPanel.SetActive(false);
    }
    public void OpenMap() 
    {
        mapPanel.SetActive(true);
        Interactions.Instance.IsMapOpen = true;
    }
    

    public void CloseMap()
    {
        mapPanel.SetActive(false);
        Interactions.Instance.IsMapOpen = false;
    }
    

    private void CollectNodesByFloor()
    {
        nodesByFloor.Clear();

        Queue<(MapNodeData node, int depth)> queue = new();
        //Add starting node to the Queue
        queue.Enqueue((MapSystem.Instance.MapData.StartingNode, 0));
        //HashSet for checking for duplicates and add starting node to it 
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

    private void SpawnNodes()
    {
        nodeViews.Clear();

        foreach (var (floor, nodes) in nodesByFloor)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                //Calculate positions x and y 
                //Position x 
                float totalWidth = (nodes.Count - 1) * nodeSpacing;
                float startX = -totalWidth / 2f;
                float xPosition = startX + i * nodeSpacing;
                //Position Y
                float yPosition = floorSpacing * floor;
                //Create a MapNodeView 
                MapNodeView mapNodeView = MapNodeViewCreator.Instance.CreateMapNodeView(node, config, nodeContainer);

                nodeViews.Add(node, mapNodeView);
                //Set anchored position
                RectTransform rt = mapNodeView.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(xPosition, yPosition);


            }
        }
    }
    private void DrawLines()
    {
        foreach( var(node,view) in nodeViews)
        {
            foreach(var child in node.ChildrenNodes)
            {
                // use anchoredPosition not position — both must be in the same coordinate space
                Vector2 parentPos = view.GetComponent<RectTransform>().anchoredPosition;
                //Get child position from nodeViews[child]'s RectTransform
                Vector2 childPos = nodeViews[child].GetComponent<RectTransform>().anchoredPosition;
                //Instantiate linePrefab parented to nodeContainer
                RectTransform lineRt = Instantiate(linePrefab, nodeContainer);
                lineRt.SetAsFirstSibling();
                //calculate midpoint
                Vector2 midPoint = (parentPos + childPos) / 2f;
                //calculate distance
                float distance = Vector2.Distance(parentPos, childPos) - linePadding;
                // Atan2 returns radians, convert to degrees for Quaternion.Euler
                Vector2 direction = childPos - parentPos;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //Apply above to lineRt
                lineRt.anchoredPosition = midPoint;
                lineRt.sizeDelta = new Vector2(distance, lineRt.sizeDelta.y);
                lineRt.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
