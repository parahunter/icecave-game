using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public float waitTime = 1f;

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
