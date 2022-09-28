using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineWarp : MonoBehaviour
{
    float length = 3.0f;
    float minSpeed = 0.01f;
    float maxSpeed = 1.0f;
    float speed = 0.0f;
    float elapsedTime = 0.0f;

    public void Start()
    {
        Destroy(gameObject, length + 3f);
    }
    void Update()
    {
        if (elapsedTime < length/2)
        {
            speed = Mathf.Lerp(maxSpeed, minSpeed, elapsedTime / (length/2));
            Time.timeScale = speed;
            elapsedTime += Time.deltaTime;
          
        }
        else
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, elapsedTime / length);
            Time.timeScale = speed;
            elapsedTime += Time.deltaTime;
        }
        
    }
}
