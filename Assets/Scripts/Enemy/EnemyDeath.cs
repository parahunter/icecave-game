using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour 
{
    public Transform enemyDeath;

    public void OnLaserHit()
    {
        Instantiate(enemyDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
