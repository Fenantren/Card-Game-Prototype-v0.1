using UnityEngine;

public class DoorViewCreator : Singleton<DoorViewCreator>
{
    [SerializeField] DoorView doorViewPrefab;

    public DoorView CreateDoorView(MapNodeData nodeData, Vector3 position, Quaternion rotation)
    {
        DoorView doorView = Instantiate(doorViewPrefab, position, rotation);
        doorView.Setup(nodeData);
        return doorView;
    }
}
