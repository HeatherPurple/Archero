using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float shootRate;
    [SerializeField] private float alpha;
    [SerializeField] private GameObject bulletPrefab;

    private Rigidbody _rigidbody;
    private InputMaster _inputMaster;
    
    private Vector2 _movement;
    private float _nextShootTime;
    
    private bool _isMoving;


    [SerializeField] private GameObject currentTarget;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputMaster = new InputMaster();
        _inputMaster.Player.Enable();
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
        _rigidbody.velocity = fixedMovement * (movementSpeed * Time.deltaTime);
    }
    
    private void AutoShoot()
    {
        if (_nextShootTime > 0) return;
        if (_isMoving) return;
        if (currentTarget is null) return;

        _nextShootTime = shootRate;
        
        var myTransform = transform;
        var targetDirection = (currentTarget.transform.position - myTransform.position).normalized;
        var bullet = Instantiate(bulletPrefab, myTransform);
        bullet.GetComponent<Bullet>().Init(targetDirection);
    }
    
}
