using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    public bool playerWon;
    public bool playerLost;

    // 0 -> game, 1 -> PlayerWinScene, 2 -> PlayerLoseScene

    private void Update()
    {
        if (playerWon)
        {
            SceneManager.LoadSceneAsync(1);
        }

        if (playerLost)
        {
            SceneManager.LoadSceneAsync(2);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && Input.GetKeyDown(KeyCode.R) ||
            SceneManager.GetActiveScene().buildIndex == 2 && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }


}
