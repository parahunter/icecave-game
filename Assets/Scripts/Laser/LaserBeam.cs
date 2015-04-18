using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserBeam 
{
    public List<LaserSegment> segments = new List<LaserSegment>();

    public Color color;
    public int bounces = 0;
    public LaserSource source;
    public bool bounced = false;

}
