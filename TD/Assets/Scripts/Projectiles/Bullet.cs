using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    private float range = 1f;
    private float damage = 1f;
    private float speed = 0f;
    private float distance = 0f;
    private int rotation;


    public void Shoot(float spd, float dmg, float rng, int rot)
    {        
        range = rng;
        speed = spd;
        damage = dmg;
        rotation = rot;

        switch (rotation)
        {
            case 0:
                transform.Rotate(Vector3.forward * 0f); // no rotation
                break;
            case 1:
                transform.Rotate(Vector3.forward * 45f);
                break;
            case 2:
                transform.Rotate(Vector3.forward * 90f);
                break;
            case 3:
                transform.Rotate(Vector3.forward * 135f);
                break;
            case 4:
                transform.Rotate(Vector3.forward * 180f);
                break;
            case 5:
                transform.Rotate(Vector3.forward * 225f);
                break;
            case 6:
                transform.Rotate(Vector3.forward * 270f);
                break;
            case 7:
                transform.Rotate(Vector3.forward * 315f);
                break;       
        }
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
        if(distance >= range){Destroy(this.gameObject);}
        transform.Translate(Vector3.up * Time.deltaTime * speed); 
        distance += Time.deltaTime * speed;   
    }
}
