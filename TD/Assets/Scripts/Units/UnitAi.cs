using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAi : MonoBehaviour
{
    public float coolDown = 2.0f;
    public float coolDownCounter = 2.0f;
    public float dmg = 2.0f;
    
    public float attackRange = 1f;




    private void DoHit()
    {
        if (coolDown <= coolDownCounter)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position,attackRange);

            foreach(Collider2D col in cols) {
                WayPoint otherHit = col.GetComponent<WayPoint>();
                if (otherHit != null)
                {
                    otherHit.gotHit(dmg);
                    coolDownCounter = 0.0f;
                    break;
                }
            }
        }
    }
      

    private void Update()
    {
        DoHit();
        if(coolDownCounter < coolDown)
        {
            coolDownCounter += Time.deltaTime;
        }
    }
}
