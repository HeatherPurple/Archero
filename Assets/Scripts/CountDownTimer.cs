using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountDownTimer : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
        StartCoroutine(CountDownCoroutine(3));
    }

    private IEnumerator CountDownCoroutine(int secondsLeft)
    {
        if (secondsLeft == 0)
        {
            Debug.Log("Start!");
            Time.timeScale = 1f;
        }
        else
        {
            Debug.Log($"{secondsLeft} seconds left!");
            yield return new WaitForSecondsRealtime(1f);
            StartCoroutine(CountDownCoroutine(secondsLeft - 1));
        }
    }
}
