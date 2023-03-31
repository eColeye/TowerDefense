using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject soldier;
    public Transform spawnPoint;
    public float spawnRate = 1f;

    private void Start()
    {
        InvokeRepeating("SpawnObject", spawnRate, spawnRate);
    }


    void SpawnObject()
    {
        if(GameManager.PlayerHP != 0)
        {
            GameObject newSoldier = Instantiate(soldier);
            Transform rt = newSoldier.GetComponent<Transform>();
            rt.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0);
        }
        else
        {
            CancelInvoke("SpawnObject");
        }
        //Debug.Log("Spawning");
    }
}
