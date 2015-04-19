using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : MonoBehaviour 
{
    public Transform player;
    public GameManager gameManager;

    public Collider2D col;

    bool opened = false;
    public ParticleSystem particles;

    void Awake()
    {
        col.isTrigger = false;
        particles.enableEmission = false;
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

}
