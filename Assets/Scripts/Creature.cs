using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected int health = 500;
    [SerializeField] protected int movementSpeed = 5;
    [SerializeField] protected int shootRate = 2; //???
    [SerializeField] protected int damage = 250;

    // [SerializeField] private float takingDamageRate;

    public virtual void TakeDamage(int takenDamage)
    {
        health -= takenDamage;
        if (health <= 0) Die();
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
