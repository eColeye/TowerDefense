using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WayPoint : MonoBehaviour
{
    private float hp = 1f;
    private Transform[] waypoints;
    private Transform parent;
    public float speed = 2.0f;
    private float hitTime;

    public float distanceVal = 0f;

    private int currentWaypoint = 1;

    private void Start()
    {
        parent = GameObject.Find("WayPoints").transform;
        waypoints = parent.GetComponentsInChildren<Transform>();
        waypoints = waypoints.Where(t => t != transform).ToArray();
    }

    public void gotHit(float dmg)
    {
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        hp -= dmg;
        hitTime = Time.time;
    }

    private void Update()
    {
        distanceVal += Time.deltaTime * speed;

        if(hitTime <= Time.time -0.1f){
            transform.GetComponent<SpriteRenderer>().color = Color.white;

        } 

        if (GameManager.PlayerHP <= 0 || hp <= 0)
        {
            Destroy(gameObject);
        }
        else if (currentWaypoint < waypoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.05f)
            {
                currentWaypoint++;
            }
        }
        else
        {
            // The enemy has reached the end of the path
            // You can add code here to damage the player's base or remove the enemy from the game
            GameManager.PlayerHP--;
            Destroy(gameObject);
            Debug.Log("Hp is now " + GameManager.PlayerHP);
        }
    }
}