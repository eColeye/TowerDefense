using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rang : MonoBehaviour
{
    private float range = 1f;
    private float damage = 1f;
    private float speed = 0f;
    private float distance = 0f;

    private float x;
    private float y;

    private bool end = false;


    public void Shoot(float spd, float dmg, float rng, float _x, float _y, float _angle)
    {        
        range = rng;
        speed = spd;
        damage = dmg;

        x = _x; 
        y = _y; 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WayPoint Hit = collider.GetComponent<WayPoint>();
        Hit.gotHit(damage);   
    }

    // Update is called once per frame
    void Update()
    {
        if(distance >= range){end = true;}
        if(distance <= -0.1f){Destroy(this.gameObject);}

        if(!end){
            transform.Translate((new Vector3(x, y, 0).normalized) * speed * Time.deltaTime);
            distance += Time.deltaTime * speed;   
        }else{
            transform.Translate((new Vector3(-x, -y, 0).normalized) * speed * Time.deltaTime);
            distance -= Time.deltaTime * speed;   
        }
    }
}
