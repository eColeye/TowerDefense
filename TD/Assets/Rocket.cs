using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Collider2D collider;
    private float damage;
    private float blastRadius;
    private float speed;
    private int i = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }


    public void Shoot(Collider2D col, float dmg, float blast, float spd)
    {
        Debug.Log("SHOOT");
        collider = col;
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

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, collider.transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, collider.transform.position) <= 0.05f)
        {
            DoHit();
            Debug.Log("Hit " + i);
            Destroy(this.gameObject);
        }
    }
}
