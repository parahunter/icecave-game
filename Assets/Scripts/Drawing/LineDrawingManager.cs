using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawingManager : MonoBehaviour 
{
    public List<GLSupplier> lineSuppliers = new List<GLSupplier>();

    public Material lineMaterial;
    
    

    public void OnPostRender()
    {
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        Matrix4x4 projection = GL.GetGPUProjectionMatrix(Camera.main.projectionMatrix, false);
        GL.LoadProjectionMatrix(projection);

        foreach (GLSupplier supplier in lineSuppliers)
        {
            supplier.Draw();
        }

        GL.PopMatrix();
        
        //playerLaserMaterial.SetPass(0);
        //GL.Begin(GL.LINES);
        ////draw laser shots

        //foreach (LaserBeam beam in laserManager.beams)
        //{
        //    foreach (LaserSegment segment in beam.segments)
        //    {
        //        GL.Vertex(segment.start3D);
        //        GL.Vertex(segment.end3D);
        //    }
        //}

    }

}
