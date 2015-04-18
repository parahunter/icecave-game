using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawCaveOutline : MonoBehaviour 
{
    public LevelGenerator levelGenerator;

    public Material caveLineMaterial;

    public void OnPostRender()
    {

        List<Vector3> outline3D = levelGenerator.outline3D;

        caveLineMaterial.SetPass(0);

        GL.PushMatrix();
        GL.LoadProjectionMatrix(Camera.main.projectionMatrix);
        GL.Begin(GL.LINES);

        for (int i = 0; i < outline3D.Count; i++)
        {
            Vector3 point = outline3D[i % outline3D.Count];
            Vector3 point2 = outline3D[(i + 1) % outline3D.Count];

            GL.Vertex(point);
            GL.Vertex(point2);
        }

        GL.End();
        GL.PopMatrix();
    }
	
}
