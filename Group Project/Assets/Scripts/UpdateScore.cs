using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
   [SerializeField] private TMP_Text scoreTxt;
   float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        scoreTxt.text = Mathf.FloorToInt(score).ToString();
    }
}
