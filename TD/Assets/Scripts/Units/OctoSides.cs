using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoSides : MonoBehaviour
{
    private float coolDownCounter = 100.0f;
    public float dmg = 2.0f;
    public  GameObject[] blast;
    public GameObject bullet;

    private bool active;
    public float attackRange = 1f;
    public float attackSpeed = 2.0f;
    public float bulletSpeed = 1f;


    //During runtime draws sphere. Switch to OnDrawGizmosSelected wanted only when selected
    private void OnDrawGizmos()
    {
        //Color newColor = new Color(1, 0.92f, 0.016f, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    //Called when an attack can happen. Attacks first target
    private void DoHit()
    {
        if (coolDownCounter >= attackSpeed)
        {
            //Send projectile
            for(int i = 0 ; i < blast.Length ; i++){
                blast[i].SetActive(true);
            }
            GameObject[] newBullet = new GameObject[8];
            Transform[] rt = new Transform[8];
            for (int i = 0; i < 8; i++)
            {
                newBullet[i] = Instantiate(bullet);
                rt[i] = newBullet[i].GetComponent<Transform>();
                rt[i].position = new Vector3(transform.position.x, transform.position.y, 0);
                rt[i].GetComponent<Bullet>().Shoot(bulletSpeed, dmg, attackRange, i);
            }            

            active = !active;
            coolDownCounter = 0;
            return;
        }
    }

    private void Update()
    {
        if (coolDownCounter > 0.2f && active)
        {
            for(int i = 0 ; i < blast.Length ; i++){
                blast[i].SetActive(false);
            }
            active = !active;
        }

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
