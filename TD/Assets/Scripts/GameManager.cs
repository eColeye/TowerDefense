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

    private static int[] SpawnNum = new int[40]
    {15, 20, 25, 30, 35,
     40, 45, 50, 55, 60,
     65, 70, 75, 80, 85,
     90, 95, 100,105,110,
     115,120,125,130,135,
     140,145,150,155,160,
     165,170,175,180,185,
     190,195,200,205,210};

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
        Debug.Log("Starting round");

        if (!roundOn)
        {
            //Checks if round is past r40 if so hard set round spawn if past r40 use calculus
            if(SpawnPoint.roundCount > 40) {
                roundOn = true;
                int x = (int)((SpawnPoint.roundCount * SpawnPoint.roundCount) / 25 + (3.5 * SpawnPoint.roundCount) + 13);
                SpawnPoint.toSpawn = x;
            }
            else
            {
                roundOn = true;
                SpawnPoint.toSpawn = SpawnNum[SpawnPoint.roundCount-1];
            }

            //makes spawning faster each round
            float y = SpawnPoint.spawnRate - 0.01f;
            if (y > 0.14f)
            {
                SpawnPoint.spawnRate = y;
            }
            else
            {
                SpawnPoint.spawnRate = 0.14f;
            }
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

    public void RoundChange(int i)
    {
        SpawnPoint.roundCount += i;
        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();
    }
}
