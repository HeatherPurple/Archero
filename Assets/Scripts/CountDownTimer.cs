using System.Collections;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private int startTime = 3;
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
        StartCoroutine(CountDownCoroutine(startTime));
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
