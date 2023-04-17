using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    //sceneIndex is 0 when home, and >0 refers to level number
    public void Swap(int sceneIndex)
    {
        string sceneName = "Level" + sceneIndex;
        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene("HomeScreen");
                return;
            case 1:
                SceneManager.LoadScene(sceneName);
                return;
            default:
                return;
        }

    }
}
