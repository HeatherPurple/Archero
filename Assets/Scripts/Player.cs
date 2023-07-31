using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : Creature
{
    public static Player Instance;
    
    [Tooltip("Min value movement input magnitude should achieve to set _isMoving value to 'True'")]
    [SerializeField] private float alpha = 0.05f;
    [SerializeField] private GameObject bulletPrefab;
    
    private int _coins;
    
    private Rigidbody _rigidbody;
    private InputMaster _inputMaster;
    private List<GameObject> _enemyList;
    private Vector2 _movement;
    
    private float _nextShootTime;
    private bool _isMoving;

    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _inputMaster = new InputMaster();
        _inputMaster.Player.Enable();

        _enemyList = transform.GetChild(0).GetComponent<EnemyDetection>().EnemyList;
        
        Enemy.EnemyDeath.AddListener(AddCoin);
    }

    private void Update()
    {
        _movement = _inputMaster.Player.Move.ReadValue<Vector2>();
        _isMoving = _rigidbody.velocity.magnitude > alpha;
    }

    private void FixedUpdate()
    {
        Move();
        AutoShoot();
    }

    private void Move()
    {
        var fixedMovement = new Vector3(_movement.x, 0f, _movement.y);
        _rigidbody.velocity = fixedMovement * movementSpeed;
    }
    
    private void AutoShoot()
    {
        _nextShootTime -= Time.fixedDeltaTime;
        
        if (_nextShootTime > 0) return;
        if (_isMoving) return;
        
        var currentTarget = GetNearestEnemy();
        if (currentTarget == null) return;
        
        _nextShootTime = shootRate;
        var myTransform = transform;
        var targetDirection = (currentTarget.transform.position - myTransform.position).normalized;
        var bullet = Instantiate(bulletPrefab, myTransform);
        bullet.GetComponent<Bullet>().Init(targetDirection, damage);
    }

    [CanBeNull]
    private GameObject GetNearestEnemy()
    {
        GameObject target = null;
        var distanceToNearestEnemy = float.MaxValue;

        foreach (var enemy in _enemyList)
        {
            if (enemy == null) continue;

            var distance = (enemy.transform.position - transform.position).magnitude;
            if (distance >= distanceToNearestEnemy) continue;
            distanceToNearestEnemy = distance;
            target = enemy;
        }

        return target;
    }
    
    private void AddCoin()
    {
        _coins++;
        Debug.Log($"You got a coin! Now your balance is {_coins}!");
    }

    private void OnDestroy()
    {
        Enemy.EnemyDeath.RemoveListener(AddCoin);
    }
}
