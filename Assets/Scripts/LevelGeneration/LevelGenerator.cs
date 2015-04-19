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

    public int minForks = 4;
    public int maxForks = 7;

    public float minControlPointDistance = 10;
    public float maxControlPointDistance = 10;


    public float outlineSubdivisions = 1.5f;
    public float minOutlineOffset = 2f;
    public float maxOutlineOffset = 3f;

    public EdgeCollider2D edgeCollider;

    public Goal goal;
    public Lock[] locks;
    public PlayerAim player;

    public float minSpawnDistanceToPlayer = 12f;

    public float enemySpawnProbability = 0.4f;

    List<Vector2> usedControlPoints = new List<Vector2>();
    
    public AnimationCurve forkChance;

    void Awake()
    {
        Random.seed = seed;
        GenerateControlPoints();
        GenerateOutline();

        List<Vector2> colliderPoints = outline;
        colliderPoints.Add(colliderPoints[0]);

        edgeCollider.points = colliderPoints.ToArray();
        goal.transform.position = controlPoints[controlPoints.Count - 1];

        PlaceStuff();

        GenerateEnemies();
    }
    
    void PlaceStuff()
    {
        player.transform.position = GetNextPlacementPoint().ToVec3();

        goal.transform.position = GetNextPlacementPoint().ToVec3();
        
        foreach(Lock l in locks)
        {
            l.transform.position = GetNextPlacementPoint().ToVec3();
        }

    }

    Vector2 GetNextPlacementPoint()
    {
        Vector2 centerOfMass = CenterOfMass();

        Vector2 point = ControlPointFarthestAway(centerOfMass);
        usedControlPoints.Add(point);

        return point;
    }

    Vector2 CenterOfMass()
    {
        Vector2 center = new Vector2();

        foreach (Vector2 vec in usedControlPoints)
            center += vec;

        if(usedControlPoints.Count > 0) 
           center /= usedControlPoints.Count;

        return center;
    }

    Vector2 ControlPointFarthestAway(Vector2 point)
    {
        float highestDistance = Mathf.NegativeInfinity;
        Vector2 furthestAway = new Vector2();

        foreach(Vector2 controlPoint in controlPoints)
        {
            if (usedControlPoints.Contains(controlPoint))
                continue;

            Vector2 vec = point - controlPoint;
            float magnitude = vec.magnitude;
            if(magnitude > highestDistance)
            {
                highestDistance = magnitude;
                furthestAway = controlPoint;
            }
        }

        return furthestAway;
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
        int forkCount = Random.Range(minForks, maxForks);

        for(int f = 0 ; f < forkCount ; f++)
        {
            float directionOffset = 0;// Random.Range(0, Mathf.PI);

            float t = (float)f / forkCount;

            float directionRadians = t * Mathf.PI * 2 + directionOffset;

            Vector2 offset = new Vector2(Mathf.Cos(directionRadians), Mathf.Sin(directionRadians)) * Random.Range(minControlPointDistance, maxControlPointDistance);

            controlPoints.Add(offset);
        }
        
    }

    void GenerateOutline()
    {

        Vector2 lastDirection = (controlPoints[0] - controlPoints[controlPoints.Count - 1]).normalized;
        for (int i = 0; i < controlPoints.Count; i++ )
        {
            Vector2 direction = (controlPoints[(i + 1) % controlPoints.Count] - controlPoints[i]);
            Vector2 normal = new Vector2(direction.normalized.y, -direction.normalized.x);
            
            Vector2 averageDirection = (lastDirection - direction.normalized).normalized;

            if (Vector2.Dot(averageDirection, normal) < 0) //convex check
                averageDirection = -averageDirection;

            Debug.DrawLine(controlPoints[i].ToVec3(), controlPoints[i].ToVec3() + averageDirection.ToVec3(), Color.green, 10f);

            outline.Add(controlPoints[i] + averageDirection * Random.RandomRange(minOutlineOffset, maxOutlineOffset));

            Debug.DrawLine(controlPoints[i].ToVec3(), controlPoints[i].ToVec3() + normal.ToVec3(), Color.cyan, 10f);

            for (float s = outlineSubdivisions; s < direction.magnitude; s += outlineSubdivisions)
            {
                Vector2 point = controlPoints[i] + direction.normalized * s;

                Vector2 offset =  normal * Random.Range(minOutlineOffset, maxOutlineOffset);

                outline.Add(point + offset);
            }

            lastDirection = direction.normalized;
        }

        foreach(Vector2 vec in outline)
        {
            outline3D.Add(vec.ToVec3());
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
