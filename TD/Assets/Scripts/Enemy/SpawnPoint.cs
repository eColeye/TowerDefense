using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    public GameObject soldier;

    public Transform spawnPoint;
    public float spawnRate = 1f;
    private float spawnCounter = 0f;

    public static int roundCount = 1;                      //Counts what round you are at
    public static int enemyAlive = 0;                      //Counts how many enemies are alive at given point
    public static int toSpawn = 0;

    private float timeCounter = 0f;
    private bool timeCount = false;

    public GameObject[] playButton;


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

            if (GameManager.autoPlay)
            {
                timeCounter = Time.time;
                timeCount = true;
            }
            else
            {
                playButton[0].GetComponent<Image>().color = Color.white;
            }
        }
        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();
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
        }
    }

    public void AutoChecker()
    {
        if (GameManager.autoPlay)
        {
            playButton[1].SetActive(false); 
            playButton[2].SetActive(true);
        }
        else
        {
            playButton[1].SetActive(true);
            playButton[2].SetActive(false);
        }
        playButton[0].GetComponent<Image>().color = Color.red;
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
        else if(timeCount && timeCounter < Time.time - 0.75f)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            GameManager.StartRound();
            timeCount = false;
        }
    }
}
