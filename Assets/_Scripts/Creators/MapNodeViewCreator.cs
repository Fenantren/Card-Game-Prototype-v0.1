using UnityEngine;

public class MapNodeViewCreator : Singleton<MapNodeViewCreator>
{
    [SerializeField] MapNodeView mapNodeViewPrefab;

    public MapNodeView CreateMapNodeView(MapNodeData nodeData, MapVisualConfig config, Transform parent)
    {
        MapNodeView mapNodeView = Instantiate(mapNodeViewPrefab, parent);

        mapNodeView.Setup(nodeData, config);

        return mapNodeView;

    }
}
