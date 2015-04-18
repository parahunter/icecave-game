using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAim : MonoBehaviour 
{
    public Vector3 direction;

    public Transform aimPivot;

    public Camera aimCamera;
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 worldPos = aimCamera.ScreenToWorldPoint(Input.mousePosition);

        direction = worldPos - transform.position;
        direction.z = 0;
        
        Vector3 normal = new Vector3(-direction.y, direction.x, 0);

        aimPivot.rotation = Quaternion.LookRotation(direction, normal);

	}
}
