using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public float duration;
    
    public void pauseGame()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;

        Transform child = gameObject.transform.GetChild(0);

        foreach (Transform t in child)
        {
            t.gameObject.GetComponent<UITweener>().OnClose();
        }
        
        Invoke("HideUI", duration);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void loadMainMenu()
    {
        // Prevent pausing affecting the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
