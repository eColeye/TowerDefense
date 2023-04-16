using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI Round;

    private void Start()
    {
        ReloadText();
    }

    public void ReloadText()
    {
        HP.text = "" + GameManager.PlayerHP;
        Money.text = "" + GameManager.GameMoney;
        Round.text = "" + SpawnPoint.roundCount;
    }
}
