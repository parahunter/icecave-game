using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShoot : LaserSource 
{
    public Gradient gradient;
    
    public MeshRenderer meshRenderer;

    public float shootDistanceFromCenter = 1.2f;
    public float triggerDistance = 15f;

    float awareness = 0;
    public float awarenessDeltaIncrease = 1;
    public float awarenessDeltaDecrease = 2;
    
    Transform player;

    RaycastHit2D[] hits;

    bool canShoot = true;
    public float cooldownPeriod = 1f;

    public AudioSource shootSource;

    // Use this for initialization
	void Start () 
    {
        hits = new RaycastHit2D[1];

        player = GameObject.FindGameObjectWithTag("Player").transform.parent;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (player == null)
            return;

        Vector2 start = new Vector2(transform.position.x, transform.position.y);

        Vector2 end = new Vector2(player.position.x, player.position.y);

        Vector2 direction = (end - start).normalized;
        start += direction * shootDistanceFromCenter;



        if ((end - start).magnitude < triggerDistance && canShoot)
        {

            int hitCount = Physics2D.LinecastNonAlloc(start, end, hits);

            if (hitCount > 0)
            {
                Transform hitTransform = hits[0].transform;
                if (hitTransform == player)
                {
                    awareness += awarenessDeltaIncrease * Time.deltaTime;
                }
            }
        }
        else
        {
            awareness -= awarenessDeltaDecrease * Time.deltaTime;
        }

        awareness = Mathf.Clamp(awareness, 0, 1.01f);

        if(awareness > 1)
        {
            Shoot(start, end);
        }

        meshRenderer.material.SetColor("_TintColor", gradient.Evaluate(awareness));
	}

    void Shoot(Vector2 enemyPos, Vector2 playerPos)
    {
        canShoot = false;
        awareness = 0;
        shootSource.Play();

        Vector2 direction = (playerPos - enemyPos).normalized;
        Vector2 beamStartPos = enemyPos;

        LaserManager.instance.AddBeam(beamStartPos, direction, this);
              

        StartCoroutine(DoCooldown());
    }

    IEnumerator DoCooldown()
    {

        yield return new WaitForSeconds(cooldownPeriod);
        canShoot = true;
    }
}
