using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void pauseGame()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void loadMainMenu()
    {
        // Prevent pausing affecting the main menu
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
