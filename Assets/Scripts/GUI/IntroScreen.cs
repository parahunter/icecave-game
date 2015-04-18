using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroScreen : MonoBehaviour 
{

    public float blinkFrequency = 1f;

    public GameObject target;
    
	// Use this for initialization
	void Start () 
    {
        StartCoroutine(Blink());
	}

	void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Screen.fullScreen = true;
            Application.LoadLevel("GameScene");
        }
    }

    IEnumerator Blink()
    {
        while(true)
        {
            yield return new WaitForSeconds(blinkFrequency);
            target.SetActive(!target.activeSelf);
        }
    }
}
