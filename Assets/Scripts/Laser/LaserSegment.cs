using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserSegment 
{
    public Vector2 start;
    public Vector2 end;
    public Vector2 direction;
    
    public Vector3 direction3D
    {
        get
        {
            return (end3D - start3D).normalized;
        }
    }

    public float length
    {
        get
        {
            return (end - start).magnitude;
        }
    }

    public Vector3 start3D
    {
        get
        {
           return new Vector3(start.x, start.y, 0); 
        }
    }

    public Vector3 end3D
    {
        get
        {
            return new Vector3(end.x, end.y, 0);
        }
    }

    public LaserSegment(Vector2 start, Vector2 direction)
    {
        this.start = start;
        this.end = start;
        this.direction = direction;

        
    }

}
