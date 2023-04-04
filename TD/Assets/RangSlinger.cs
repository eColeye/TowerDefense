using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangSlinger : MonoBehaviour
{
    private float coolDownCounter = 100.0f;
    public float dmg = 2.0f;
    public GameObject rang;

    public float attackRange = 1f;
    public float attackSpeed = 2.0f;
    public float rangSpeed = 1f;


    //During runtime draws sphere. Switch to OnDrawGizmosSelected wanted only when selected
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //Called when an attack can happen. Attacks first target
    private void DoHit()
    {
        Collider2D col = getMax();
        if (coolDownCounter >= attackSpeed && col != null)
        {
            //Grabs x,y component. Rotate object and store x, y
            float y = col.transform.position.y - transform.position.y;
            float x = col.transform.position.x - transform.position.x;

            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle -90f);

            //Send projectile
            GameObject newRang = Instantiate(rang);
            Transform rt = newRang.GetComponent<Transform>();
            rt.position = new Vector3(transform.position.x, transform.position.y, 0);
            rt.GetComponent<Rang>().Shoot(rangSpeed, dmg, attackRange, x, y, angle);

            coolDownCounter = 0;
            return;
        }
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
                if (col != null)
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

        if (coolDownCounter < attackSpeed)
        {
            coolDownCounter = coolDownCounter + Time.deltaTime;
        }
        else
        {
            DoHit();
        }
    }
}
