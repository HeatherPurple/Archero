using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected int MovementSpeed;
    [SerializeField] protected int ShootRate;
    [SerializeField] protected int Damage;

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) Die();
    }
    
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Destroy(gameObject);
    }
}