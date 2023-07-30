using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player component))
        {
            Debug.Log("Win!");
            Time.timeScale = 0f;
        }
    }
}
