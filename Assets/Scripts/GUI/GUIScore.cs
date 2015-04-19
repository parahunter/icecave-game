using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIScore : MonoBehaviour 
{
    public Text cavesCompletedText;

    void Start()
    {
        cavesCompletedText.text = GameManager.instance.completedCaves.ToString();
    }
	
}
