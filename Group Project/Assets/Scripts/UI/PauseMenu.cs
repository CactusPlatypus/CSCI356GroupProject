using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private float duration = 0.15f;
    
    public void pauseGame()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;

        // Hacky, loop through 2 layers of children
        foreach (Transform t1 in transform)
        {
            UITweener tweener = t1.gameObject.GetComponent<UITweener>();
            if (tweener) tweener.OnClose();
            foreach (Transform t2 in t1)
            {
                tweener = t2.gameObject.GetComponent<UITweener>();
                if (tweener) tweener.OnClose();
            }
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
