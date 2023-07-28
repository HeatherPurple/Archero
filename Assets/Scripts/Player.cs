using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Bullet> ammo = new List<Bullet>();

    private Rigidbody rigidbody;
    private InputMaster _inputMaster;
    private Vector2 movement;
    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        _inputMaster = new InputMaster();
        _inputMaster.Player.Enable();
        
        _inputMaster.Player.Shoot.performed += Shoot;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("Space was pressed!");
        
    }


    private void Update()
    {
        movement = _inputMaster.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();   
    }

    private void Move()
    {
        var fixedMovement = new Vector3(movement.x, 0f, movement.y);
        rigidbody.velocity = fixedMovement * (speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        _inputMaster.Player.Shoot.performed -= Shoot;
    }
}
