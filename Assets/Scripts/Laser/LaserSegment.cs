using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserSegment 
{
    public Vector2 start;
    public Vector2 end;

    public Vector3 start3D;
    public Vector3 end3D;

    public LaserSegment(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;

        start3D = new Vector3(start.x, start.y, 0);
        end3D = new Vector3(end.x, end.y, 0);
    }

}
