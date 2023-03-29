using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAi : MonoBehaviour
{
    public float coolDown = 2.0f;
    public float coolDownCounter = 2.0f;
    public float dmg = 2.0f;




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (coolDown <= coolDownCounter)
        {
            WayPoint otherHit = other.GetComponent<WayPoint>();
            if (otherHit != null)
            {
                otherHit.gotHit(dmg);
                coolDownCounter = 0.0f;
            }
        }
    }
      

    private void Update()
    {
        if(coolDownCounter < coolDown)
        {
            coolDownCounter += Time.deltaTime;
        }
    }
}
