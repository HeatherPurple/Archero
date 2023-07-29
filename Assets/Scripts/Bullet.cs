using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int damage;

    [SerializeField] private float speed;
    [SerializeField] private BulletTarget ignoreTarget;
    
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 velocity, int damage)
    {
        _rigidbody.velocity = velocity * speed;
        this.damage = damage;
        //rotate
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ignoreTarget.ToString())) return;
        
        if (other.gameObject.TryGetComponent(out Creature component)) component.TakeDamage(damage);

        Destroy(gameObject);
    }
}

public enum BulletTarget
{
    Player,
    Enemy
}
