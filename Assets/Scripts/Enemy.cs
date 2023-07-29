using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] private float _movementDistance;
    [SerializeField] private float sleepTime;
    [SerializeField] private bool isFlying;

    [SerializeField] private EnemyState currentState = EnemyState.Sleeping;

    private void Awake()
    {
        currentState = EnemyState.Sleeping;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                Move();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Sleeping:
                Sleep();
                break;
        }
    }

    private void Move()
    {
        
    }
    
    private void Attack()
    {
        
    }
    
    private void Sleep()
    {
        
    }

    protected override void Die()
    {
        base.Die();
    }

    enum EnemyState
    {
        Moving,
        Attacking,
        Sleeping
    }
}


