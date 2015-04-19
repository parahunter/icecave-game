using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPLayer : MonoBehaviour 
{

    public AudioSource music;

    public static MusicPLayer instance;

	// Use this for initialization
	void Start () 
    {
        if (instance == null)
        {
            instance = this;
            music.Play();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}
	
}
