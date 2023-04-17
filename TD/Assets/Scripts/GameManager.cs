using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerHP = 50;

    public static int GameMoney = 5;

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
            int x = SpawnPoint.roundCount * 4 + 3;
            SpawnPoint.toSpawn = x;

            Debug.Log("toSpawn start = " + x);
        }
        else
        {
            Debug.Log("AutoPlay started");
            autoPlay = !autoPlay;
        }

        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();
    }

    public void CheatMoney()
    {
        GameMoney += 100;
        TextUpdater textUpdater = FindObjectOfType<TextUpdater>();
        textUpdater.ReloadText();
    }
}
