using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airplane : MonoBehaviour
{
    public Transform target;
    public GameObject plane;
    public GameObject shadow;
    public GameObject bullet;

    public float planeSpeed = 1f;
    public float planeAttackRange = 4f;
    public float planeFlightradius = 2f;

    public float attackSpeed = 0.2f;
    public float attackCounter = 0f;
    public float bulletSpeed = 1f;

    public bool active = false;

    public float damage = 1f;

    public float angle = 0f;
    private float x;
    private float y;
    
    
    
    //During runtime draws sphere. Switch to OnDrawGizmosSelected wanted only when selected
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(plane.transform.position, planeAttackRange);
    }


    private void doShot()
    {
        Collider2D max = getMax();
        if(max != null)
        {
            try
            {
                attackCounter = 0f;
                GameObject newBullet = Instantiate(bullet);
                Transform bt = newBullet.GetComponent<Transform>();
                bt.position = new Vector3(plane.transform.position.x, plane.transform.position.y, 0);
                bt.GetComponent<trackingBullet>().Shoot(max, damage, bulletSpeed);
            }catch(System.NullReferenceException){}
        }
    }


    private Collider2D getMax()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(plane.transform.position, planeAttackRange);
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

    void Update()
    {
        if (active)
        {
            if (attackCounter >= attackSpeed)
            {
                doShot();
            }
            else
            {
                attackCounter += Time.deltaTime;
            }



            angle += planeSpeed * Time.deltaTime;

            plane.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg + 90f);
            shadow.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg + 90f);

            float x = planeFlightradius * Mathf.Cos(angle) + target.position.x;
            float y = planeFlightradius * Mathf.Sin(angle) + target.position.y;

            plane.transform.position = new Vector3(x, y, plane.transform.position.z);
            shadow.transform.position = new Vector3(plane.transform.position.x, plane.transform.position.y - 0.75f, plane.transform.position.z);
        }
    }
}
