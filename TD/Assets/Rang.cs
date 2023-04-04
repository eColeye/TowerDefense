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
    private float angle;

    Vector3 direction;

    private bool end = false;


    public void Shoot(float spd, float dmg, float rng, float _x, float _y, float _angle)
    {        
        range = rng;
        speed = spd;
        damage = dmg;

        direction = new Vector3(x, y, 0).normalized;

        x = _x; 
        y = _y;
        angle = _angle;        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WayPoint Hit = collider.GetComponent<WayPoint>();
        Hit.gotHit(damage);   
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((new Vector3(x, y, 0).normalized) * speed * Time.deltaTime);

        // if(distance >= range){end = true;}
        // if(distance <= -0.1f){Destroy(this.gameObject);}

        // if(!end){
        //     transform.Translate(direction * speed * Time.deltaTime);
        //     distance += Time.deltaTime * speed;   
        // }else{
        //     transform.Translate(Vector3.down * Time.deltaTime * speed); 
        //     distance -= Time.deltaTime * speed;   
            
        // }
    }
}
