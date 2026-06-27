using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct RoomTypeSprite
{
    public RoomType RoomType;
    public Sprite Sprite;
}
public class DoorView : MonoBehaviour
{
    [SerializeField] private List<RoomTypeSprite> roomTypeSprites;

    [SerializeField] SpriteRenderer doorSprite;

    [SerializeField] GameObject wrapper;

    MapNodeData nodeData;

    public void Setup(MapNodeData nodeData)
    {
        this.nodeData = nodeData;
        foreach (var entry in roomTypeSprites)
        {
            if (entry.RoomType == nodeData.RoomType)
            {
                doorSprite.sprite = entry.Sprite;
                break;
            }
        }
    }

    private void OnMouseEnter()
    {
        wrapper.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    private void OnMouseExit()
    {
        wrapper.transform.localScale = Vector3.one;
    }

    private void OnMouseDown()
    {
        MapSystem.Instance.TravelToNode(nodeData);

        switch (nodeData.RoomType)
        {
            case RoomType.RCOMBAT:
            case RoomType.ECOMBAT:
                SceneManager.LoadScene(SceneNames.Combat);
                break;
            
            case RoomType.BOSS:
                SceneManager.LoadScene(SceneNames.Boss);
                break;
            
            case RoomType.REST:
                SceneManager.LoadScene(SceneNames.Rest); 
                break;
           
            case RoomType.TREASURE:
                SceneManager.LoadScene(SceneNames.Treasure);
                break;
        }
    }
}
