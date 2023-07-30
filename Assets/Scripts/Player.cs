using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : Creature
{
    public static Player Instance;
    
    [SerializeField] private float alpha;
    [SerializeField] private GameObject bulletPrefab;

    private Rigidbody _rigidbody;
    private InputMaster _inputMaster;
    
    private Vector2 _movement;
    private float _nextShootTime;
    
    private bool _isMoving;

    //[SerializeField] private GameObject currentTarget;
    [SerializeField] private List<GameObject> enemyList;


    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _inputMaster = new InputMaster();
        _inputMaster.Player.Enable();

        enemyList = transform.GetChild(0).GetComponent<EnemyDetection>().EnemyList;
    }

    private void Update()
    {
        _movement = _inputMaster.Player.Move.ReadValue<Vector2>();
        _isMoving = _rigidbody.velocity.magnitude > alpha;
        _nextShootTime -= Time.deltaTime;
        
        AutoShoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var fixedMovement = new Vector3(_movement.x, 0f, _movement.y);
        _rigidbody.velocity = fixedMovement * (MovementSpeed * Time.deltaTime);
    }
    
    private void AutoShoot()
    {
        if (_nextShootTime > 0) return;
        if (_isMoving) return;
        
        var currentTarget = GetNearestEnemy();
        if (currentTarget == null) return;
        
        _nextShootTime = ShootRate;
        var myTransform = transform;
        var targetDirection = (currentTarget.transform.position - myTransform.position).normalized;
        var bullet = Instantiate(bulletPrefab, myTransform);
        bullet.GetComponent<Bullet>().Init(targetDirection, Damage);
    }

    [CanBeNull]
    private GameObject GetNearestEnemy()
    {
        GameObject target = null;
        var distanceToNearestEnemy = float.MaxValue;

        foreach (var enemy in enemyList)
        {
            if (enemy == null) continue;
            //check if it's possible to attack enemy
            
            var distance = (enemy.transform.position - transform.position).magnitude;
            if (distance >= distanceToNearestEnemy) continue;
            distanceToNearestEnemy = distance;
            target = enemy;
        }

        return target;
    }


}
