using UnityEngine;
using UnityEngine.UI;

public class MapNodeView : MonoBehaviour
{
    

    [SerializeField] MapNodeData nodeData;

    [SerializeField] Image roomImage;

    public void Setup(MapNodeData nodeData, MapVisualConfig config)
    {
        this.nodeData = nodeData;

        roomImage.sprite = config.GetSpriteForRoomType(nodeData.RoomType);

        if (nodeData == MapSystem.Instance.CurrentNode)
        {
            roomImage.color = config.CurrentColour;
        }
        else if (MapSystem.Instance.IsNodeVisited(nodeData) )
        {
            roomImage.color = config.VisitedColour;
        }
        else if (MapSystem.Instance.CurrentNode.ChildrenNodes.Contains(nodeData))
        {
            roomImage.color = config.AvailableColour;
        }
        else
        {
            roomImage.color = config.LockedColour;
        }

    }
}
