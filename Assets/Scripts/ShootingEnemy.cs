using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingEnemy : Enemy
{
    [SerializeField] private float roamingDistance = 2f;
    [Tooltip("Min distance to _roamingPoint to stop roaming")]
    [SerializeField] private float alpha = 0.05f;
    [SerializeField] private GameObject bulletPrefab;

    private Vector3 _roamingPoint;
    private Vector3 _roamingDirection;
    private bool _isRoaming;
    
    private ShootingEnemyState _currentShootingEnemyState;

    private void Awake()
    {
        _currentShootingEnemyState = ShootingEnemyState.Roaming;
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (_currentShootingEnemyState)
        {
            case ShootingEnemyState.Sleeping:
                Sleeping();
                break;
            case ShootingEnemyState.Roaming:
                Roaming();
                break;
            case ShootingEnemyState.Attacking:
                Attacking();
                break;
        }
        
    }

    private void Sleeping()
    {
        Rigidbody.velocity = Vector3.zero;
        NextMoveTime -= Time.fixedDeltaTime;
        if (NextMoveTime > 0) return;

        NextMoveTime = sleepTime;
        _currentShootingEnemyState = ShootingEnemyState.Roaming;
    }


    private void Roaming()
    {
        if (!_isRoaming)
        {
            var randomAngle = Random.Range(0f, 2 * Mathf.PI);
            var position = transform.position;
            _roamingPoint = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized * roamingDistance +
                           new Vector3(0f,position.y,0f);
            _roamingDirection = (_roamingPoint - position).normalized;
            _isRoaming = true;
        }

        Rigidbody.velocity = _roamingDirection * movementSpeed;
        
        if (Vector3.Distance(transform.position,_roamingPoint) <= alpha)
        {
            _isRoaming = false;
            _currentShootingEnemyState = ShootingEnemyState.Attacking;
        }
    }


    private void Attacking()
    {
        if (!IsAttacking)
        {
            Rigidbody.velocity = Vector3.zero;
            AttackTimeLeft = attackTime;
            IsAttacking = true;
        }
        
        //rotate to the player
        AttackTimeLeft -= Time.fixedDeltaTime;
        if (AttackTimeLeft > 0) return;

        var myTransform = transform;
        var targetDirection = (PlayerTransform.position - myTransform.position).normalized;
        var bullet = Instantiate(bulletPrefab, myTransform);
        bullet.GetComponent<Bullet>().Init(targetDirection, damage);

        NextAttackTime = Time.time + attackRate;
        IsAttacking = false;
        _currentShootingEnemyState = ShootingEnemyState.Sleeping;
    }


    private enum ShootingEnemyState
    {
        Sleeping,
        Roaming,
        Attacking
    }

}