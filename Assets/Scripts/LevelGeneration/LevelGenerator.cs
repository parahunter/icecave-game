using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour 
{
    public int seed = 0;

    List<Vector2> controlPoints = new List<Vector2>();
    List<Vector2> outline = new List<Vector2>();

    public int minControlPoints = 4;
    public int maxControlPoints = 7;

    public float minHorizontalSeparation = -4f;
    public float maxHorizontalSeparation = 4f;

    public float minVerticalSeparation = -2f;
    public float maxVerticalSeparation = 2f;

    public float outlineSubdivisions = 1.5f;
    public float minOutlineOffset = 2f;
    public float maxOutlineOffset = 3f;

    public EdgeCollider2D edgeCollider;

    void Awake()
    {
        Random.seed = seed;
        GenerateControlPoints();
        GenerateOutline();

        edgeCollider.points = outline.ToArray();

    }
	
    void OnDrawGizmos()
    {
        //control points
        Gizmos.color = Color.red;
        if (controlPoints.Count > 0)
        {
            for (int i = 0; i < controlPoints.Count - 1; i++)
            {
                Gizmos.DrawSphere(controlPoints[i], 0.1f);
                Gizmos.DrawLine(controlPoints[i], controlPoints[(i + 1)]);
            }
        }

        //outline
        Gizmos.color = Color.white;
        if (outline.Count > 0)
        {
            for (int i = 0; i < outline.Count; i++)
            {
                Gizmos.DrawSphere(outline[i], 0.1f);
                Gizmos.DrawLine(outline[i], outline[(i + 1) % outline.Count]);
            }
        }
    }

    void GenerateControlPoints()
    {
        int controlPointsCount = Random.Range(minControlPoints, maxControlPoints);

        controlPoints.Add(Vector2.zero);

        for(int i = 1 ; i < controlPointsCount ; i++)
        {
            Vector2 old = controlPoints[i - 1];

            Vector2 offset = new Vector2(Random.Range(minHorizontalSeparation, maxHorizontalSeparation), Random.Range(minVerticalSeparation, maxVerticalSeparation));

            controlPoints.Add(old + offset);
        }
    }

    void GenerateOutline()
    {
        //left
        outline.Add(controlPoints[0] - Vector2.right * Random.Range(minOutlineOffset, maxOutlineOffset));

        //lower part
        for (int i = 0; i < controlPoints.Count - 1; i++)
        {
            Vector2 direction = controlPoints[i + 1] - controlPoints[i];
            
            for (float s = 0; s < direction.magnitude ; s += outlineSubdivisions)
            {
                Vector2 point = controlPoints[i] + direction.normalized * s;

                Vector2 offset = -Vector2.up * Random.Range(minOutlineOffset, maxOutlineOffset);

                outline.Add(point + offset);
            }
        }

        //right
        outline.Add(controlPoints[controlPoints.Count - 1] + Vector2.right * Random.Range(minOutlineOffset, maxOutlineOffset));

        //top
        for (int i = controlPoints.Count - 1; i > 0 ; i--)
        {
            Vector2 direction = controlPoints[i - 1] - controlPoints[i];

            for (float s = 0; s < direction.magnitude; s += outlineSubdivisions)
            {
                Vector2 point = controlPoints[i] + direction.normalized * s;

                Vector2 offset = Vector2.up * Random.Range(minOutlineOffset, maxOutlineOffset);

                outline.Add(point + offset);
            }
        }
 
    }

    void OnPostRenderer()
    {

    }
}
