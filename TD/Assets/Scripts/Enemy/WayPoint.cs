using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WayPoint : MonoBehaviour
{
    public float hp = 1f;                  //Hp of enemy
    private Transform[] waypoints;          //All waypoints to follow               
    public float speed = 2.0f;              //Speed of enemy
    public int enemyMoneyValue = 1;         //Value of killing enemy
    private float hitTime;                  //Value to keep track of color swapping enemy after hit

    public float distanceVal = 0f;          //Calculating the distance object has traveled
    private int currentWaypoint = 1;        //Keeps track of current waypoint
    private bool left = false;



    //Function to give calls to reload all game texts and kill the enemy
    public void Reload()
    {
        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();

        SpawnPoint spawnPoint = FindObjectOfType<SpawnPoint>();
        spawnPoint.KillEnemy();
    }

    private void Start()
    {
        Transform parent = GameObject.Find("WayPoints").transform;
        waypoints = parent.GetComponentsInChildren<Transform>();
        waypoints = waypoints.Where(t => t != transform).ToArray();
        getDirrection();
    }

    //Gives dmg to the unit if hit
    public void gotHit(float dmg)
    {
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        hp -= dmg;
        hitTime = Time.time;
    }

    public void getDirrection()
    {
        float y = waypoints[currentWaypoint].transform.position.y - transform.position.y;
        float x = waypoints[currentWaypoint].transform.position.x - transform.position.x;

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        if(270f > Mathf.Abs(angle) && Mathf.Abs(angle) > 90f && !left)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            left = true;
        }
        else if(left)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            left = false;
        }
    }

    private void Update()
    {
        //Checks if game is paused first
        if (!GameManager.Paused)
        {
            distanceVal += Time.deltaTime * speed;

            //Reset target color if needed
            if (hitTime <= Time.time - 0.1f)
            {
                transform.GetComponent<SpriteRenderer>().color = Color.white;
            }

            //Checks if player is defeated
            if (GameManager.PlayerHP <= 0)
            {
                Destroy(gameObject);
            }
            //Checks if Enemy is defeated
            else if (hp <= 0)
            {
                Destroy(gameObject);
                GameManager.GameMoney += enemyMoneyValue;
                Reload();
            }
            //Checks if there are more waypoints
            else if (currentWaypoint < waypoints.Length)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

                //Checks if reached waypoint
                if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.05f)
                {
                    currentWaypoint++;
                    if(currentWaypoint < waypoints.Length)
                    {
                        getDirrection();
                    }
                }
            }
            else
            {
                // The enemy has reached the end of the path
                // You can add code here to damage the player's base or remove the enemy from the game
                GameManager.PlayerHP-= 1;
                Reload();
                Destroy(gameObject);
            }
        }
    }
}