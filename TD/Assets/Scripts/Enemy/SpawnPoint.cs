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
