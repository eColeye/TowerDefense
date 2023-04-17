using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    private float coolDownCounter = 2.0f;
    public float dmg = 2.0f;
    public  GameObject blast;

    public float attackRange = 1f;
    public float attackSpeed = 2.0f;

    public bool active = false;


    //During runtime draws sphere. Switch to OnDrawGizmosSelected wanted only when selected
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    //Called when an attack can happen. Attacks first target
    private void DoHit()
    {
        Collider2D colMax = getMax();
        try
        {
            if (colMax != null && coolDownCounter >= attackSpeed)
            {
                doRotate(colMax);
                blast.SetActive(true);
                colMax.GetComponent<WayPoint>().gotHit(dmg);
                coolDownCounter = 0;
                return;
            }
            else
            {
                doRotate(colMax);
            }
        }catch(System.NullReferenceException){
        }
    }

    //Take x and y component to find z angle. Then rotates 
    private void doRotate(Collider2D col)
    {
        float y = col.transform.position.y - transform.position.y;
        float x = col.transform.position.x - transform.position.x;

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    //From the [] of colliders in attackRange and get the one which has traveled the longest distance
    private Collider2D getMax()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackRange);
        Collider2D colMax = null;


        foreach (Collider2D col in cols)
        {
            try
            {
                if (col != null && col.GetComponent<WayPoint>() != null)
                {
                    if(colMax== null)
                    {
                        colMax = col; 
                    }
                    else if (colMax.GetComponent<WayPoint>().distanceVal <= col.GetComponent<WayPoint>().distanceVal)
                    {
                        colMax = col;
                    }
                }
                            
            }
            catch (System.NullReferenceException)
            {               
            }
        }
        return colMax;
    }


    private void Update()
    {
        if (active)
        {
            if (coolDownCounter > 0.2f)
            {
                blast.SetActive(false);
            }
            coolDownCounter = coolDownCounter + Time.deltaTime;
            DoHit();
        }
    }
}
