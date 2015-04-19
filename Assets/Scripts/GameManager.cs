using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public float waitTime = 1f;
    public Transform player;

    public LevelGenerator levelGenerator;

    public int completedCaves = 0;

    public static GameManager instance;

    public int lockCount = 0;
    public int lockAmount = 3;
    public Goal goal;

    void Awake()
    {
        instance = this;

        GameObject newGameObj = GameObject.FindGameObjectWithTag("NewGameMessage");
        GameObject completedLevelObj = GameObject.FindGameObjectWithTag("CompletedLevelMessage");

        if(newGameObj != null)
        {
            SeedLevelGenerator();

            Destroy(newGameObj);
        }

        if(completedLevelObj != null)
        {
            SeedLevelGenerator();

            CompletedLevelMessage message = completedLevelObj.GetComponent<CompletedLevelMessage>();

            completedCaves = message.completedCaves;

            Destroy(completedLevelObj);
        }

        
    }

    void SeedLevelGenerator()
    {
        int seed = (int)System.DateTime.Now.ToBinary();

        levelGenerator.seed = seed;
    }

    public void CompleteLevel()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerDeath>().enabled = false;
        player.GetComponent<PlayerAim>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerJumping>().enabled = false;
        player.Find("Collider").gameObject.SetActive(false);

        StartCoroutine(AnimateCompleteLevel());
    }
    
    public void GameOver()
    {
        StartCoroutine(waitThenReload());
    }

    public void IncreaseLockCount()
    {
        lockCount++;

        if(lockCount == lockAmount)
        {
            goal.Open();
        }
    }



    IEnumerator waitThenReload()
    {
        yield return new WaitForSeconds(waitTime);

        GameObject obj = new GameObject("GameOverMessage");
        obj.tag = "GameOverMessage";
        DontDestroyOnLoad(obj);
        GameOverMessage message = obj.AddComponent<GameOverMessage>();
        message.completedCaves = this.completedCaves;

        Application.LoadLevel("GameOverScene");
    }

    IEnumerator AnimateCompleteLevel()
    {
        yield return new WaitForSeconds(waitTime);

        GameObject obj = new GameObject("CompletedLevelMessage");
        obj.tag = "CompletedLevelMessage";
        DontDestroyOnLoad(obj);
        CompletedLevelMessage message = obj.AddComponent<CompletedLevelMessage>();
        message.completedCaves = this.completedCaves + 1;

        Application.LoadLevel(Application.loadedLevelName);
    }
}
