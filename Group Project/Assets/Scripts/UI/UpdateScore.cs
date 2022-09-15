using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private float score = 0;

    void Update()
    {
        score += Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
