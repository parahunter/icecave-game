using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerJumping : MonoBehaviour 
{
    public float jumpForce = 3;
    public ForceMode2D forceMode = ForceMode2D.Impulse;

    Rigidbody2D body;

    public ParticleSystem particleSystem;
    public float emissionRatePerThrust = 20;

	// Use this for initialization
	void Start () 
    {
        body = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        float input = Input.GetAxis("Jump");

        particleSystem.emissionRate = emissionRatePerThrust * input;

        body.AddForce(Vector2.up * input * jumpForce, forceMode);


	}
}
