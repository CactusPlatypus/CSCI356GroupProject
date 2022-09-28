using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineWarp : MonoBehaviour
{
    private const float length = 3f;
    private const float minSpeed = 0.01f;
    private const float maxSpeed = 1f;
    private float elapsedTime = 0f;

    public void Start()
    {
        Destroy(gameObject, length + 3f);
    }
    void Update()
    {
        if (elapsedTime < length / 2f)
        {
            float speed = Mathf.Lerp(maxSpeed, minSpeed, elapsedTime / (length / 2f));
            Time.timeScale = speed;
            elapsedTime += Time.deltaTime;
        }
        else
        {
            float speed = Mathf.Lerp(minSpeed, maxSpeed, elapsedTime / length);
            Time.timeScale = speed;
            elapsedTime += Time.deltaTime;
        }
    }
}
