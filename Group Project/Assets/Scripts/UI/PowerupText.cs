using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupText : MonoBehaviour
{
    [SerializeField] private GameObject powerupCanvas;
    [SerializeField] private TMP_Text powerupText;

    private const float hideTime = 3f;

    public void ShowText(string text)
    {
        powerupText.text = text;
        powerupCanvas.SetActive(true);
        Invoke("CloseText", hideTime * 0.5f);
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
