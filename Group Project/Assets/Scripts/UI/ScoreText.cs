using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private TMP_Text scoreText;
    private float score = 0;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        score += Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
