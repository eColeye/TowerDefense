using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Collider2D target;
    private float damage;
    private float blastRadius;
    private float speed;
    private int i = 0;
    private float lifeTime = 5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }


    public void Shoot(Collider2D col, float dmg, float blast, float spd)
    {
        Debug.Log("SHOOT");
        target = col;
        damage = dmg;
        blastRadius = blast;
        speed = spd;
    }
    private void DoHit()
    {           
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        foreach (Collider2D col in cols)
        {
            WayPoint otherHit = col.GetComponent<WayPoint>();
            if (otherHit != null)
            {
                i++;
                otherHit.gotHit(damage);                    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision hit " + i);
        DoHit();
        Destroy(this.gameObject);
    }

    private void aim()
    {
        float y = target.transform.position.y - transform.position.y;
        float x = target.transform.position.x - transform.position.x;

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    public void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            lifeTime -= Time.deltaTime;
            aim();
        }        

        //Each object has a max lifeline 
        if (lifeTime <= 0){Destroy(this.gameObject);}
    }
}
