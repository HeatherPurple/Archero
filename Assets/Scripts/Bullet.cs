using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float speed;
    [SerializeField] private BulletTarget ignoreTarget;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 velocity)
    {
        _rigidbody.velocity = velocity * speed;
        //rotate
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ignoreTarget.ToString())) return;


        Destroy(gameObject);


            // collision.gameObject.TryGetComponent(out Player component) ? component.name.Length;

    }
    

    private enum BulletTarget
    {
        Player,
        Enemy
    }
}
