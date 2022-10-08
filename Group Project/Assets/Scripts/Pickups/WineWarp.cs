using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineWarp : MonoBehaviour
{
    private const float duration = 5f;
    private const float minSpeed = 0.25f;
    private const float maxSpeed = 1f;
    private float elapsedTime = 0f;

    private void Update()
    {
        // Don't update when the menu is open
        if (Time.timeScale <= 0f) return;

        // Sine wave shaped time curve
        float curve = 0.5f + Mathf.Cos(2 * Mathf.PI * elapsedTime / duration) * 0.5f;
        float speed = Mathf.Lerp(minSpeed, maxSpeed, curve);
        Time.timeScale = speed;

        // Use unscaledDeltaTime since deltaTime is affected by timeScale
        elapsedTime += Time.unscaledDeltaTime;
        if (elapsedTime >= duration)
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }
}
