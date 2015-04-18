using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserManager : MonoBehaviour 
{
    public List<LaserBeam> beams = new List<LaserBeam>();

    public float laserLength = 1;
    public float laserSpeed = 3;
    public float laserStartSpeedStart = 5f;
    public float laserStartSpeedEnd = 7f;


    RaycastHit2D[] hits;
    public int hitsCount = 5;
    public string bounceTag = "BouncesLasers";
    public string killTag = "DestroyedByLasers";
    public float hitPadding = 0.01f;
    

    void Awake()
    {
        hits = new RaycastHit2D[hitsCount];


    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        foreach(LaserBeam beam in beams)
        {
            bool removeStartSegment = false;



            //move beams
            LaserSegment startSegment= beam.segments[0];
            LaserSegment endSegment = beam.segments[beam.segments.Count - 1];

            float speed = beam.bounced ? laserSpeed : laserStartSpeedEnd;

            endSegment.end += endSegment.direction * speed * Time.fixedDeltaTime;

            float beamLength = 0;
            foreach (LaserSegment segment in beam.segments)
                beamLength += segment.length;

            if (!beam.bounced)
            {
                speed = laserStartSpeedStart;
            }
            else if (beamLength > laserLength)
            {
                speed = laserSpeed;
            }
            else if (startSegment != endSegment)
                speed = laserSpeed;

            Vector2 delta = startSegment.direction * speed * Time.fixedDeltaTime;
            Vector2 proxy = startSegment.start + delta;

            Vector2 proxyToEnd = (startSegment.end - proxy).normalized;

            if (Vector2.Dot(startSegment.direction, proxyToEnd) < 0.5f)
            {
                removeStartSegment = true;
            }
            else
                startSegment.start += delta;


            
            //beam.segments[0] = startSegment;
            //beam.segments[beam.segments.Count - 1] = endSegment;

            //see if beam should bounce on walls or objects
            int hitResultCount = Physics2D.LinecastNonAlloc (endSegment.start, endSegment.end, hits );
            for(int i = 0 ; i < hitResultCount ; i++)
            {
                RaycastHit2D hit = hits[i];
                if(hit.transform.CompareTag(bounceTag))
                {
                    beam.bounced = true;
                  //  Debug.Break();
                    
                    //clamp segment to wall
                    endSegment.end = hit.point - endSegment.direction * hitPadding;

                    Vector3 normal = new Vector3(hit.normal.x, hit.normal.y, 0);

  //                  if (hit.fraction == 0)
//                        normal = -normal;

                    Vector3 outDir = Vector3.Reflect(endSegment.direction3D, normal);

                    LaserSegment bouncedSegment = new LaserSegment(endSegment.end, new Vector2(outDir.x, outDir.y));

                    beam.segments.Add(bouncedSegment);

                    break;
                }
            }

            //hit detection
            foreach(LaserSegment segment in beam.segments)
            {
                hitResultCount = Physics2D.LinecastNonAlloc (endSegment.start, endSegment.end, hits );
            
                for(int i = 0 ; i < hitResultCount ; i++)
                {
                    RaycastHit2D hit = hits[i];
                    if (hit.transform.CompareTag(killTag))
                    {
                        hit.transform.SendMessageUpwards("OnLaserHit", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

            if (removeStartSegment)
                beam.segments.RemoveAt(0);
        }
	}

    public void AddBeam( Vector3 position, Vector3 direction, LaserSource source)
    {

        LaserBeam beam = new LaserBeam();

        beam.segments.Add(new LaserSegment(position, direction));
        beam.source = source;
        beam.bounced = false;

        beams.Add(beam);
    }


}
