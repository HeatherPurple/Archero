using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public readonly List<GameObject> EnemyList = new List<GameObject>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy component)) EnemyList.Add(component.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy component)) EnemyList.Remove(component.gameObject);
    }
}

