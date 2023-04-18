using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerHP = 50;

    public static int GameMoney = 5;

    public static int SpawnRateMod = 0;

    public static bool Paused = false;

    public static bool roundOn = false;

    public static bool autoPlay = false;

    public static Dictionary<string, int> unitCost = new Dictionary<string, int>()
    {
        {"Turret", 200},
        {"Gunner", 300},
        {"Booma", 400},
        {"Octo", 500},
        {"Rocket", 600},
        {"Sniper", 700},
        {"Plane", 800},
    };

    public static void StartRound()
    {
        if (!roundOn)
        {
            roundOn = true;
            int x = (int)((SpawnPoint.roundCount * SpawnPoint.roundCount) / 25 + (3.5 * SpawnPoint.roundCount) + 13);
            SpawnPoint.toSpawn = x;

            float y = SpawnPoint.spawnRate - 0.015f;
            if(y > 0.14f)
            {
                SpawnPoint.spawnRate = y;
            }
            else
            {
                SpawnPoint.spawnRate = 0.14f;
            }

            Debug.Log("toSpawn start = " + x + " with spawn rate " + SpawnPoint.spawnRate);
        }
        else
        {
            autoPlay = !autoPlay;
        }

        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();
    }

    public void CheatMoney()
    {
        GameMoney += 10000;
        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();
    }
}
