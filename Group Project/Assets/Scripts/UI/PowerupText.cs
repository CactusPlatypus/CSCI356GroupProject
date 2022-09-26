using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupText : MonoBehaviour
{
    [SerializeField] private GameObject powerupCanvas;
    [SerializeField] private TMP_Text powerupText;
    float maxTextSize = 28.3f;
    float minTextSize = 0.0f;
    float timeElapsed = 0.0f;
    float powerupTime = 0.0f;
    float fontsize = 0;

    public void showText(string text, float time)
    {
        powerupText.text = text;
        powerupText.fontSize = 0;
        powerupTime = time;
        powerupCanvas.SetActive(true);
        Invoke("hideText", time);
        fontsize = 0.0f;
        timeElapsed = 0.0f;
        



    }
    private void Update()
    {
        //lerp the font size
        fontsize = Mathf.Lerp(minTextSize, maxTextSize, timeElapsed / powerupTime);
        powerupText.fontSize = fontsize;
        timeElapsed += Time.deltaTime;
        
    }
    public void hideText()
    {
        powerupCanvas.SetActive(false);
    }

}
