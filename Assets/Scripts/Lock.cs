using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lock : MonoBehaviour 
{


    void OnLaserHit()
    {
        GameManager.instance.IncreaseLockCount();

        Destroy(gameObject);
    }

}
