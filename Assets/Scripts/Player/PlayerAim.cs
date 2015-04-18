using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAim : MonoBehaviour 
{
    public Vector3 direction;

    public Transform aimPivot;

    public Camera aimCamera;
    public Camera viewCamera;
    public float viewInterpolation = 0.8f;

    public float viewMaxDistanceOffset = 3;

    public float interpolationTime = 0.3f;
    Vector3 interpolationVelocity;
    
	// Update is called once per frame
	void Update () 
    {
        Vector3 aimPos = aimCamera.ScreenToWorldPoint(Input.mousePosition);

        direction = aimPos - transform.position;
        direction.z = 0;
        
        Vector3 normal = new Vector3(-direction.y, direction.x, 0);

        aimPivot.rotation = Quaternion.LookRotation(direction, normal);




        Vector3 offset = (aimPos - transform.position);
        offset = Vector3.ClampMagnitude(offset, viewMaxDistanceOffset);
        
        Vector3 cameraPos = offset * viewInterpolation + transform.position;
        cameraPos.z = viewCamera.transform.position.z;

        Vector3 localCameraPos = transform.worldToLocalMatrix * offset;
        localCameraPos.z = viewCamera.transform.localPosition.z;

        Vector3 interPolatedPosition = Vector3.SmoothDamp(viewCamera.transform.localPosition, localCameraPos, ref interpolationVelocity, interpolationTime);

        viewCamera.transform.localPosition = interPolatedPosition;
	}
}
