using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuntime : MonoBehaviour
{
    public GameObject PausedMenu;
    public GameObject RunTime;


    /*    public void StartRound()
        {
            GameManager.Paused = false;

        }*/


    public void PauseSwitch()
    {
        PausedMenu.SetActive(!PausedMenu.activeSelf);
        RunTime.SetActive(!PausedMenu.activeSelf);
        GameManager.Paused = PausedMenu.activeSelf;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausedMenu.SetActive(!PausedMenu.activeSelf);
            RunTime.SetActive(!PausedMenu.activeSelf);
            GameManager.Paused = PausedMenu.activeSelf;
        }
    }
}
