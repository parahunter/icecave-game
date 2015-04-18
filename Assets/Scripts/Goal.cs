using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal : MonoBehaviour 
{
    public Transform player;
    public GameManager gameManager;

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
