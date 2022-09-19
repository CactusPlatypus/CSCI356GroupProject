using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public int coins;

    void Update() {
        this.GetComponent<UnityEngine.UI.Text>().text = "Coins: " + coins;
    }
    public void add(int i) {
        coins = coins + i;
        GetComponent<AudioSource>().Play();
    }
}
