using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Vector2Int FieldSize;
    [SerializeField] private GameObject FieldBlockPrefab;

    void Start()
    {
        for (int i = 0; i < FieldSize.x; i++)
        {
            for (int j = 0; j < FieldSize.y; j++)
            {
                if (i == 0)
                {
                    Instantiate(FieldBlockPrefab,
                        new Vector3(i - FieldSize.x / 2 - 1, 1f, j - FieldSize.y/2), new Quaternion());
                }if (i == FieldSize.x - 1)
                {
                    Instantiate(FieldBlockPrefab,
                        new Vector3(i - FieldSize.x / 2 + 1, 1f, j - FieldSize.y/2), new Quaternion());
                }
                if (j == 0)
                {
                    Instantiate(FieldBlockPrefab,
                        new Vector3(i - FieldSize.x / 2, 1f, j - FieldSize.y/2 -1), new Quaternion());
                }if (j == FieldSize.y - 1)
                {
                    Instantiate(FieldBlockPrefab,
                        new Vector3(i - FieldSize.x / 2,1f, j - FieldSize.y/2 +1), new Quaternion());
                }
                Instantiate(FieldBlockPrefab,
                    new Vector3(i - FieldSize.x / 2, 0f, j - FieldSize.y/2), new Quaternion());
            }
        }
    }
    
}
