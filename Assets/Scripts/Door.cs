using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    void Awake()
    {
        Spawner.EndGame.AddListener(OpenDoor);
    }

    private void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Spawner.EndGame.AddListener(OpenDoor);
    }
}
