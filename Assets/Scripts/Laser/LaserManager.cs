using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserManager : MonoBehaviour 
{
    public List<LaserBeam> beams = new List<LaserBeam>();

    public float laserLength = 1;
    public float laserSpeed = 3;
        
	// Update is called once per frame
	void FixedUpdate () 
    {
	    
	}

    public void AddBeam( Vector3 position, Vector3 direction, LaserSource source)
    {

        LaserBeam beam = new LaserBeam();

        beam.segments.Add(new LaserSegment(position, position + direction * laserLength));
        beam.source = source;

        beams.Add(beam);

    }


}
