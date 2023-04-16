using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject soldier;

    public Transform spawnPoint;
    public float spawnRate = 1f;
    private float spawnCounter = 0f;

    public static int roundCount = 1;              //Counts what round you are at
    public int enemyAlive = 0;              //Counts how many enemies are alive at given point
    public static int toSpawn = 4;   //change to array for each round

    //An enemy died, sub enemy counter
    public void KillEnemy() 
    {
        enemyAlive--;

        Debug.Log("Enemies left = " +  enemyAlive);
        CheckDead();
    }

    //Functon to check if all enemies are dead in the round
    private void CheckDead()
    {
        if(enemyAlive == 0 && toSpawn == 0)
        {
            //Round end
            roundCount++;
            GameManager.roundOn = false;

            Debug.Log("Round is now " + roundCount);

            TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
            textUpdater.ReloadText();
        }
    }

    private void SpawnObject()
    {
        if(GameManager.PlayerHP != 0)
        {
            toSpawn--;
            GameObject newSoldier = Instantiate(soldier);
            Transform rt = newSoldier.GetComponent<Transform>();
            rt.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0);
            enemyAlive++;

            Debug.Log("toSpawn = " + toSpawn);
        }
    }

    private void Update ()
    {
        if (!GameManager.Paused && GameManager.roundOn && toSpawn != 0)
        {
            spawnCounter = spawnCounter + Time.deltaTime;
            if(spawnCounter >= spawnRate)
            {
                SpawnObject();
                spawnCounter = 0f;
            }
        }
    }
}
