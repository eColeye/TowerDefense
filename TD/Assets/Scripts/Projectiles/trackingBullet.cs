using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingBullet : MonoBehaviour
{
    private Collider2D target;
    private Collider2D hit;

    private float damage;
    private float bulletSpeed;


    public void Shoot(Collider2D col, float dmg, float spd)
    {
        target = col;
        damage = dmg;
        bulletSpeed = spd;
    }


    private void DoHit()
    {           
        try
        {
            WayPoint otherHit = hit.GetComponent<WayPoint>();
            if(otherHit != null){
                otherHit.gotHit(damage);
            }
        }catch(System.NullReferenceException){}
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision hit ");
        hit = collider;
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

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            aim();
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }    
    }
}
