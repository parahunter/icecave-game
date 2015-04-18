using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawCaveOutline : MonoBehaviour 
{
    public LevelGenerator levelGenerator;

    public Material caveLineMaterial;
    public Material playerLaserMaterial;

    public LaserManager laserManager;

    void Update()
    {

    }

    public void OnPostRender()
    {
        List<Vector3> outline3D = levelGenerator.outline3D;

        caveLineMaterial.SetPass(0);

        GL.PushMatrix();
        Matrix4x4 projection = GL.GetGPUProjectionMatrix(Camera.main.projectionMatrix, false);
        GL.LoadProjectionMatrix(projection);
        GL.Begin(GL.LINES);

        //draw level outline
        for (int i = 0; i < outline3D.Count; i++)
        {
            Vector3 point = outline3D[i % outline3D.Count];
            Vector3 point2 = outline3D[(i + 1) % outline3D.Count];

            GL.Vertex(point);
            GL.Vertex(point2);
        }
        GL.End();

        playerLaserMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        //draw laser shots
        
        foreach (LaserBeam beam in laserManager.beams)
        {
            foreach(LaserSegment segment in beam.segments)
            {
                GL.Vertex(segment.start3D);
                GL.Vertex(segment.end3D);
            }
        }


        GL.End();
        GL.PopMatrix();
    }
	
}
