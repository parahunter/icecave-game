using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOverScreen : MonoBehaviour 
{
    public Text score;

	// Use this for initialization
	void Start () 
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameOverMessage");

        if(obj != null)
        {
            GameOverMessage message = obj.GetComponent<GameOverMessage>();
             score.text = message.completedCaves.ToString();

            Destroy(obj);
        }

	}

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
       //     Screen.fullScreen = true;

            GameObject obj = new GameObject("NewGameMessage");
            obj.tag = "NewGameMessage";
            DontDestroyOnLoad(obj);
            NewGameMessage message = obj.AddComponent<NewGameMessage>();

            Application.LoadLevel("GameScene");
        }
    }
}
