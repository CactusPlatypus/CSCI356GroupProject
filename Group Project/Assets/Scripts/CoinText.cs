using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    public int coins;

    public void add(int i) {
        coins = coins + i;
        this.GetComponent<TMP_Text>().text = coins.ToString();
    }
}
