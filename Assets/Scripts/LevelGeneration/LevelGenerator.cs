using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : GLSupplier 
{
    public int seed = 0;
    public Color outlineColor = Color.white;

    public List<Vector2> controlPoints = new List<Vector2>();
    public List<Vector2> outline = new List<Vector2>();
    public List<Vector3> outline3D = new List<Vector3>();

    public Transform basicEnemyPrefab;

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

    public Goal goal;

    public float minSpawnDistanceToPlayer = 12f;

    public float enemySpawnProbability = 0.4f;

    void Awake()
    {
        Random.seed = seed;
        GenerateControlPoints();
        GenerateOutline();

        List<Vector2> colliderPoints = outline;
        colliderPoints.Add(colliderPoints[0]);

        edgeCollider.points = colliderPoints.ToArray();
        goal.transform.position = controlPoints[controlPoints.Count - 1];

        GenerateEnemies();
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
 
        foreach(Vector2 point in outline)
        {
            outline3D.Add(new Vector3(point.x, point.y, 0));
        }
    }

    void GenerateEnemies()
    {
        
        for(int i = 0 ; i < outline.Count - 2 ; i++)
        {
            if (Random.Range(0, 1f) > enemySpawnProbability)
                continue;

            Vector2 startPoint = outline[i];
            Vector2 endPoint = outline[i + 1];

            //player always starts at origo so make sure enemies are further back
            if(startPoint.magnitude > minSpawnDistanceToPlayer && endPoint.magnitude > minSpawnDistanceToPlayer)
            {
                Vector2 direction = (endPoint - startPoint);

                Vector2 spawnPoint = startPoint + direction * Random.Range(0, 0.5f);

                Instantiate(basicEnemyPrefab, new Vector3(spawnPoint.x, spawnPoint.y, 0), Quaternion.identity);
            }

        }

    }

    public override void Draw()
    {
        GL.Begin(GL.LINES);
        GL.Color(outlineColor);

        //draw level outline
        for (int i = 0; i < outline3D.Count; i++)
        {
            Vector3 point = outline3D[i % outline3D.Count];
            Vector3 point2 = outline3D[(i + 1) % outline3D.Count];

            GL.Vertex(point);
            GL.Vertex(point2);
        }
        GL.End();
    }
}
