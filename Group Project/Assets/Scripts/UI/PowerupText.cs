using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupText : MonoBehaviour
{
    public LeanTweenType easeIn;
    public LeanTweenType easeOut;

    private GameObject powerupText;
    private AudioSource sound;
    private Coroutine timer;
    private const float hideTime = 2f;

    private void Start()
    {
        powerupText = transform.GetChild(0).gameObject;
        sound = GetComponent<AudioSource>();
    }

    public void ShowText(string text)
    {
        // Don't hide text, just show it
        if (timer != null) StopCoroutine(timer);

        // Animate manually
        powerupText.transform.localScale = new Vector3(0f, 0f, 0f);
        LeanTween.scale(powerupText, new Vector3(1f, 1f, 1f), 0.5f)
            .setEase(easeIn)
            .setIgnoreTimeScale(true);

        powerupText.GetComponent<TMP_Text>().text = text;
        sound.Play();
        
        // Hide text after time
        timer = StartCoroutine(CloseText());
    }

    public IEnumerator CloseText()
    {
        yield return new WaitForSeconds(hideTime);
        LeanTween.scale(powerupText, new Vector3(0f, 0f, 0f), 0.5f)
            .setEase(easeOut)
            .setIgnoreTimeScale(true);
    }
}
