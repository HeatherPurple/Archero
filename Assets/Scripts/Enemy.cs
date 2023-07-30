using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Enemy : Creature
{
    [SerializeField] private float _movementDistance;
    [SerializeField] private float sleepTime;
    [SerializeField] private bool isFlying;

    [SerializeField] private float attackDistance;
    [SerializeField] private float attackMovementSpeedMultiplier;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackRate;

    [SerializeField] private EnemyState _currentState;

    private Rigidbody _rigidbody;
    private Transform _player;
    private float _nextMoveTime;
    private float _nextAttackTime;
    private float _endAttackTime;
    private bool _isAttacking;


    public static UnityEvent enemySpawn = new UnityEvent();
    public static UnityEvent enemyDeath = new UnityEvent();
    

    private void Awake()
    {
        _currentState = EnemyState.Chasing;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _player = Player.Instance.transform;
        enemySpawn.Invoke();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.Sleeping:
                Sleeping();
                break;
            case EnemyState.Chasing:
                Chasing();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }

    private void Sleeping()
    {
        _rigidbody.velocity = Vector3.zero;
        _nextMoveTime -= Time.deltaTime;
        if (_nextMoveTime > 0) return;

        _nextMoveTime = sleepTime;
        _currentState = EnemyState.Chasing;
    }

    private void Chasing()
    {
        var targetDirection = (_player.position - transform.position).normalized;
        _rigidbody.velocity = targetDirection * (MovementSpeed * Time.deltaTime);

        if (Time.time > _nextAttackTime &&
            Vector3.Distance(_player.position, transform.position) <= attackDistance)
            _currentState = EnemyState.Attacking;
    }


    private void Attacking()
    {
        if (!_isAttacking)
        {
            _endAttackTime = attackTime;
            var targetDirection = (_player.position - transform.position).normalized;
            _rigidbody.velocity = targetDirection * (MovementSpeed * attackMovementSpeedMultiplier * Time.deltaTime);
            _isAttacking = true;
        }

        _endAttackTime -= Time.deltaTime;
        if (_endAttackTime > 0) return;

        _nextAttackTime = Time.time + attackRate;
        _isAttacking = false;
        _endAttackTime = attackTime;
        _currentState = EnemyState.Sleeping;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player component)) component.TakeDamage(Damage);
    }

    protected override void Die()
    {
        enemyDeath.Invoke();
        base.Die();
    }


    private enum EnemyState
    {
        Sleeping,
        Chasing,
        Attacking
    }

}