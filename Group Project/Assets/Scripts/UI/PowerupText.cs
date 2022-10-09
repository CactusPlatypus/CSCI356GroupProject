using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupText : MonoBehaviour
{
    [SerializeField] private GameObject powerupCanvas;
    [SerializeField] private TMP_Text powerupText;

    private const float minTextSize = 0f;
    private const float maxTextSize = 100f;

    private const float scaleTime = 0.25f;
    private const float hideTime = 3f;

    private float timeElapsed = 0f;

    /*private void Update()
    {
        if (timeElapsed < scaleTime)
        {
            float grow = timeElapsed / scaleTime;
            powerupText.fontSize = Mathf.Lerp(minTextSize, maxTextSize, grow);
        }
        else if (timeElapsed > hideTime - scaleTime)
        {
            float shrink = (timeElapsed - hideTime + scaleTime) / scaleTime;
            powerupText.fontSize = Mathf.Lerp(maxTextSize, minTextSize, shrink);
        }
        
        timeElapsed += Time.deltaTime;
    }*/

    public void ShowText(string text)
    {
        powerupText.text = text;
        // powerupText.fontSize = 0f;
        // timeElapsed = 0f;
        powerupCanvas.SetActive(true);
        Invoke("CloseText", hideTime/2);
        Invoke("HideText", hideTime);
    }

    public void CloseText()
    {
        powerupText.GetComponent<UITweener>().OnClose();
    }
    
    public void HideText()
    {
        powerupCanvas.SetActive(false);
    }
}
