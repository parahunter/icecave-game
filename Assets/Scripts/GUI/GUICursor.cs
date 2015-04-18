using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUICursor : MonoBehaviour 
{
    
    RectTransform rectTransform;

	// Use this for initialization
	void Start () 
    {
	    rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 mousePos = Input.mousePosition;

        rectTransform.position = mousePos;
	}
}
