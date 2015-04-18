using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public float waitTime = 1f;
    public Transform player;


    
    public void CompleteLevel()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerDeath>().enabled = false;
        player.GetComponent<PlayerAim>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerJumping>().enabled = false;
        player.Find("Collider").gameObject.SetActive(false);

                   

    }


    

    public void GameOver()
    {
        StartCoroutine(waitThenReload());
    }


    IEnumerator waitThenReload()
    {
        yield return new WaitForSeconds(waitTime);

        Application.LoadLevel(Application.loadedLevelName);
    }
}
