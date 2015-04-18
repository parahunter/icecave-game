using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour 
{
    public float horizontalForce = 3;
    public ForceMode2D forceMode = ForceMode2D.Impulse;

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Vector2 moveForce = Vector2.right * Input.GetAxis("Horizontal") * horizontalForce;

        body.AddForce(moveForce, forceMode);
	}
}
