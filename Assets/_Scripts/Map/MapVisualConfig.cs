using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MapVisualConfig")]
public class MapVisualConfig : ScriptableObject
{
    [field: SerializeField] public List<RoomTypeSprite> RoomTypeSprites { get; private set; }

    [field: SerializeField] public Color VisitedColour { get; private set; }
    [field: SerializeField] public Color CurrentColour { get; private set; }
    [field: SerializeField] public Color AvailableColour { get; private set; }
    [field: SerializeField] public Color LockedColour { get; private set; }

    public Sprite GetSpriteForRoomType(RoomType roomType)
    {
        foreach (var entry in  RoomTypeSprites)
        {
            if (entry.RoomType == roomType)
            {
                return entry.Sprite;
                
            }
            
        }
        return default;
    }
}
