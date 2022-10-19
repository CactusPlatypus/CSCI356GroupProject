using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    // Menu displaying settings button, either pause menu or main menu
    public GameObject hostMenu;

    public void showSettings()
    {
        gameObject.SetActive(true);
        hostMenu.SetActive(false);
    }

    public void hideSettings()
    {
        gameObject.SetActive(false);
        hostMenu.SetActive(true);
    }

    public void loadGame()
    {
        SceneManager.LoadScene("RealGame");
    }

    public void quitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
