using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject soldier;
    public Transform spawnPoint;

    private void Start()
    {
        InvokeRepeating("SpawnObject", 1f, 1f);
    }


    void SpawnObject()
    {
        if(GameManager.PlayerHP != 0)
        {
            Instantiate(soldier, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            CancelInvoke("SpawnObject");
        }
        Debug.Log("Spawning");
    }
}
