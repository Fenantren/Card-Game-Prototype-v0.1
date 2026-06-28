using System.Collections.Generic;
using UnityEngine;

public class LobbyBoardView : MonoBehaviour
{
    [SerializeField] float spacing = 2f;

    public List<DoorView> DoorViews { get; private set; } = new();

    private void Start()
    {
        List<MapNodeData> childrenNodes = MapSystem.Instance.CurrentNode.ChildrenNodes;

        int doorCount = childrenNodes.Count;
        

        for( int i = 0; i < doorCount; i++)
        {
            Vector3 position = GetDoorPosition(i, doorCount);
            DoorView doorView = DoorViewCreator.Instance.CreateDoorView(childrenNodes[i], position, transform.rotation);
            DoorViews.Add(doorView);
        }
    }

    private Vector3 GetDoorPosition(int index, int totalCount)
    {
        float totalWidth = (totalCount - 1) * spacing;
        float startX = -totalWidth / 2f;
        float xPosition = startX + index * spacing;

        return new Vector3(xPosition, transform.position.y, transform.position.z);
    }
}
