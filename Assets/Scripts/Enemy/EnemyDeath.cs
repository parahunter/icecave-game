using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour 
{

    public void OnLaserHit()
    {

        Destroy(gameObject);
    }
}
