using MoreMountains.TopDownEngine;
using UnityEngine;

public class Orientation2D : CharacterOrientation2D
{
    public Vector2 GetFacingDirection()
    {
        return new Vector2(_horizontalDirection, _verticalDirection);
    }
}
