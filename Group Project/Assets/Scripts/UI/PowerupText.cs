using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupText : MonoBehaviour
{
    [SerializeField] private GameObject powerupCanvas;
    [SerializeField] private TMP_Text powerupText;

    private const float minTextSize = 0f;
    private const float maxTextSize = 28.3f;
    private const float powerupTime = 2f;
    private float timeElapsed = 0f;

    private void Update()
    {
        // Lerp the font size
        float fontSize = Mathf.Lerp(minTextSize, maxTextSize, timeElapsed / powerupTime);
        powerupText.fontSize = fontSize;
        timeElapsed += Time.deltaTime;
    }

    public void ShowText(string text)
    {
        powerupText.text = text;
        powerupText.fontSize = 0f;
        timeElapsed = 0f;
        powerupCanvas.SetActive(true);
        Invoke("HideText", 4f);
    }

    public void HideText()
    {
        powerupCanvas.SetActive(false);
    }
}
