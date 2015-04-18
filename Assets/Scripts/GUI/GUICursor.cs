using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUICursor : MonoBehaviour 
{
    
    RectTransform rectTransform;
    public PlayerAim playerAim;

	// Use this for initialization
	void Start () 
    {
	    rectTransform = GetComponent<RectTransform>();
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(playerAim != null)
        {
            Vector3 mousePos = Camera.main.WorldToScreenPoint(playerAim.aimPos);

            rectTransform.position = mousePos;
        }


	}
}
