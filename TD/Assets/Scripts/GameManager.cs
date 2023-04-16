using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerHP = 50;

    public static int GameMoney = 5;

    public static bool Paused = false;

    public static bool roundOn = false;




    public void StartRound()
    {
        if (!roundOn)
        {
            roundOn = true;
            int x = SpawnPoint.roundCount * 4 + 3;
            SpawnPoint.toSpawn = x;
            Debug.Log("toSpawn start = " + x);
        }
    }
}
