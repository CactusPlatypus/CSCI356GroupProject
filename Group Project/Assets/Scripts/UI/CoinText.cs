using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    private TMP_Text text;
    private AudioSource source;
    private int coins = 0;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        source = GetComponent<AudioSource>();
    }

    public void add(int count) {
        coins += count;
        text.text = coins.ToString();
        // Audio is played from the text since the coin deletes itself and any audio sources attached
        source.Play();
    }
}
