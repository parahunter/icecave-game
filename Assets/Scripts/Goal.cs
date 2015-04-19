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

    void Start()
    {
        col.isTrigger = false;
        particles.enableEmission = false;

        float radius = col.radius;
        for (int i = 0; i < subdivissions; i++)
        {
            float t = (float)i/ subdivissions;
            t *= 2 * Mathf.PI;
            Vector3 offset = new Vector3(Mathf.Cos(t), Mathf.Sin(t), 0) * radius;
            circleOutline.Add(offset + transform.position);    
        }
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
        GL.Begin(GL.LINES);
        GL.Color(color);

        for (int o = 0; o < circleOutline.Count; o++ )
        {
            GL.Vertex(circleOutline[o]);
            GL.Vertex(circleOutline[(o + 1) % circleOutline.Count]);
        }
        
        GL.End();
    }
}
