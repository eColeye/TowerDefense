using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public TextMeshProUGUI money;
    public TextMeshProUGUI round;
    public TextMeshProUGUI remaining;

    public TextMeshProUGUI[] shopUnits;
    /*
     * 0 : Turret
     * 1 : Gunner
     * 2 : Booma
     * 3 : Octo
     * 4 : Rocket
     * 5 : Sniper
     * 6 : Plane
     */

    private string[] unit = new string[] {
        "Turret",
        "Gunner",
        "Booma",
        "Octo",
        "Rocket",
        "Sniper",
        "Plane"
    };

    private void Start()
    {
        ReloadText();
    }

    public void ReloadText()
    {
        HP.text = "" + GameManager.PlayerHP;
        money.text = "" + GameManager.GameMoney;
        round.text = "Round: " + SpawnPoint.roundCount;
        remaining.text = "Remaining: " + (SpawnPoint.enemyAlive + SpawnPoint.toSpawn); 

        for (int i = 0; i < shopUnits.Length ; i++)
        {
            int cost = GameManager.unitCost[unit[i]];
            shopUnits[i].text = "" + cost;
            if(cost > GameManager.GameMoney)
            {
                shopUnits[i].color = Color.red;
            }
            else
            {
                shopUnits[i].color = Color.black;
            }

        }        
    }
}
