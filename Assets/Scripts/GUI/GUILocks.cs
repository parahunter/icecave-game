using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUILocks : MonoBehaviour 
{
    
    public Image[] fullImages;
	
	// Update is called once per frame
	void Update () 
    {
	    for(int i = 0 ; i < fullImages.Length ; i++)
        {
            if (GameManager.instance.lockCount > i)
                fullImages[i].enabled = true;
            else
                fullImages[i].enabled = false;
        }


	}
}
