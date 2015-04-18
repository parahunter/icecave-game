using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDeath : MonoBehaviour 
{
    public Camera mainCamera;
    public GameManager gameManager;

    public void OnLaserHit()
    {
        mainCamera.transform.parent = null;

        Destroy(gameObject);

        gameManager.GameOver();

    }


}
