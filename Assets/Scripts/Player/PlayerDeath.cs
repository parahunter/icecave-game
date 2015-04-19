using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDeath : MonoBehaviour 
{
    public Camera mainCamera;
    public GameManager gameManager;
    public Transform playerDeath;

    public void OnLaserHit()
    {
        mainCamera.transform.parent = null;
        Instantiate(playerDeath, transform.position, Quaternion.identity);

        Destroy(gameObject);

        gameManager.GameOver();

    }


}
