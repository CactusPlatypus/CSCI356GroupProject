using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITweener : MonoBehaviour
{
    public LeanTweenType easeIn;
    public LeanTweenType easeOut;
    public float duration;
    public float delay;

    public void OnEnable()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), duration)
            .setDelay(delay)
            .setEase(easeIn)
            .setIgnoreTimeScale(true);
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), duration)
            .setEase(easeOut)
            .setIgnoreTimeScale(true);
    }
}
