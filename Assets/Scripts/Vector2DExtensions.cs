using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vector2DExtensions
{
    public static Vector3 ToVec3(this Vector2 vec)
    {
        return new Vector3(vec.x, vec.y, 0);
    }
}
