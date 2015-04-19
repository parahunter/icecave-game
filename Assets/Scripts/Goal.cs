using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : GLSupplier 
{
    public Transform player;
    public GameManager gameManager;

    public CircleCollider2D col;
    public int subdivissions = 30;

    bool opened = false;
    public Color color;
    public ParticleSystem particles;

    List<Vector3> circleOutline = new List<Vector3>();

    Vector3 cross0;
    Vector3 cross1;
    Vector3 cross2;
    Vector3 cross3;


    void Start()
    {
        col.isTrigger = false;
        particles.enableEmission = false;
        
        
        Vector3 offset;
        float radius = col.radius;
        for (int i = 0; i < subdivissions; i++)
        {
            float t = (float)i/ subdivissions;
            t *= 2 * Mathf.PI;
            offset = new Vector3(Mathf.Cos(t), Mathf.Sin(t), 0) * radius;
            circleOutline.Add(offset + transform.position);    
        }

        float radians = Mathf.Deg2Rad * 45f;

        offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
        cross0 = offset + transform.position;
        cross1 = -offset + transform.position;

        radians += Mathf.PI * 0.5f;

        offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
        cross2 = offset + transform.position;
        cross3 = -offset + transform.position;    

    }

    public void Open()
    {
        col.isTrigger = true;
        opened = true;
        particles.enableEmission = true;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.root != null)
        {
            if(collider.transform.root == player)
            {
                gameManager.CompleteLevel();
            }
        }
    }


    public override void Draw()
    {
        if (opened)
            return;

        GL.Begin(GL.LINES);
        GL.Color(color);

        for (int o = 0; o < circleOutline.Count; o++ )
        {
            GL.Vertex(circleOutline[o]);
            GL.Vertex(circleOutline[(o + 1) % circleOutline.Count]);
        }

        GL.Vertex(cross0);
        GL.Vertex(cross1);

        GL.Vertex(cross2);
        GL.Vertex(cross3);

        GL.End();
    }
}
