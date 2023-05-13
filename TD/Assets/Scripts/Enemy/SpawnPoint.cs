using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    public GameObject soldier;

    public GameObject[] enemies;
    /*
     * 0 : Green
     * 1 : Blue
     * 2 : Pink
     * 3
     * 4
     * 5
     */


    private string[] waveParts;
    private bool hasSplit = false;
    private List<int> keyList = new List<int>();
    private List<int> spawnList = new List<int>();

    //Stored by     enemy | spawnNum : enemy | spawnNum... 
    private string[] waveSpawner = new string[40] {
        "0|15",
        "0|9:1|1:0|9:1|1",
        "0|9:1|1:0|5:2|2",
        "0|30",
        "0|35",
        "0|40",
        "0|45",
        "0|50",
        "0|55",
        "0|60",
        "0|65",
        "0|70",
        "0|75",
        "0|80",
        "0|85",
        "0|90",
        "0|95",
        "0|100",
        "0|105",
        "0|110",
        "0|115",
        "0|120",
        "0|125",
        "0|130",
        "0|135",
        "0|140",
        "0|145",
        "0|150",
        "0|155",
        "0|160",
        "0|165",
        "0|170",
        "0|175",
        "0|180",
        "0|185",
        "0|190",
        "0|195",
        "0|200",
        "0|205",
        "0|210",
    };

    public Transform spawnPoint;
    private float spawnCounter = 0f;

    public static float spawnRate = 0.85f;
    public static int roundCount = 1;                      //Counts what round you are at
    public static int enemyAlive = 0;                      //Counts how many enemies are alive at given point
    public static int toSpawn = 0;
    public static int spawned = 0;

    private float timeCounter = 0f;
    private bool timeCount = false;
    private float spawnRange = 14f;


    public GameObject[] playButton;


    //An enemy died, sub enemy counter
    public void KillEnemy() 
    {
        enemyAlive--;
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
            hasSplit = false;
        }

        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();
    }

    private void Split()
    {
        Debug.Log("waveSpawner " + waveSpawner[roundCount - 1]);
        if (roundCount <= 40)
        {
            waveParts = waveSpawner[roundCount - 1].Split(':');
            foreach (string part in waveParts)
            {
                string[] newPart = part.Split('|');
                keyList.Add(int.Parse(newPart[0]));
                spawnList.Add(int.Parse(newPart[1]));
            }

            Debug.Log("keyList " + keyList[0]);
            Debug.Log("Spawnlist " + spawnList[0]);
        }
        hasSplit = true;
    }

    private void SpawnObject()
    {
        if(GameManager.PlayerHP != 0 && keyList.Count > 0)
        {
            Debug.Log("IN SPAWN OBJECT");
            if (spawnList[0] > 0)
            {
                SpawnObject(keyList[0]);
                spawnList[0] -= 1;

                if(spawnList[0] <= 0)
                {
                    keyList.Remove(keyList[0]);
                    spawnList.Remove(spawnList[0]);
                }
                if(keyList.Count > 0)
                {
                    Debug.Log("Key is " + keyList + " with size of " + keyList.Count + "  spawn is " + spawnList[0]);
                }       
                for(int i = 0; i < keyList.Count; i++)
                {
                    Debug.Log("keyList has " + keyList[i]);
                }
            }
        }
        else if(GameManager.PlayerHP != 0 && toSpawn > 0) 
        {
            //If mistake was made and need to spawn more. Spawn basic enemies
            SpawnObject(-1);
        }

    }

    public void SpawnObject(int key)
    {
        switch(key)
        {
            case 0:
                toSpawn--;
                Instantiate(enemies[key], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
                enemyAlive++;
                spawned++;
                return;
            case 1:
                toSpawn--;
                Instantiate(enemies[key], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
                enemyAlive++;
                spawned++;
                return; 
            case 2:
                toSpawn--;
                Instantiate(enemies[key], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
                enemyAlive++;
                spawned++;
                return;
            case 3:
                toSpawn--;
                Instantiate(enemies[key], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
                enemyAlive++;
                spawned++;
                return;
            case 4:
                toSpawn--;
                Instantiate(enemies[key], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
                enemyAlive++;
                spawned++;
                return;
            case 5:
                toSpawn--;
                Instantiate(enemies[key], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
                enemyAlive++;
                spawned++;
                return;
            default:
                Debug.Log("IN DEFAULT SPAWN");
                toSpawn--;
                Instantiate(enemies[0], new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
                enemyAlive++;
                spawned++;
                return;
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
        //Debug.Log(!GameManager.Paused + "    " + GameManager.roundOn + "    " + toSpawn);
        if (!GameManager.Paused && GameManager.roundOn && toSpawn != 0)
        {
            if(!hasSplit){Split();}

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
