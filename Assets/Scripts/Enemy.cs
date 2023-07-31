using UnityEngine;
using UnityEngine.Events;

public class Enemy : Creature
{
    [SerializeField] protected float sleepTime = 0.2f;
    [SerializeField] protected float attackTime = 0.5f;
    [SerializeField] protected float attackRate = 2f;

    [SerializeField] private float attackDistance = 4f;
    [SerializeField] private float attackMovementSpeedMultiplier = 2.2f; 
    
    public static readonly UnityEvent EnemyDeath = new UnityEvent();
    
    protected Rigidbody Rigidbody;
    protected Transform PlayerTransform;
    
    protected float NextMoveTime;
    protected float NextAttackTime;
    protected float AttackTimeLeft;
    protected bool IsAttacking;
    
    private EnemyState _currentEnemyState;

    private void Awake()
    {
        _currentEnemyState = EnemyState.Chasing;
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        PlayerTransform = Player.Instance.transform;
    }
    
    private void FixedUpdate()
    {
        switch (_currentEnemyState)
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
        Rigidbody.velocity = Vector3.zero;
        NextMoveTime -= Time.fixedDeltaTime;
        if (NextMoveTime > 0) return;

        NextMoveTime = sleepTime;
        _currentEnemyState = EnemyState.Chasing;
    }

    private void Chasing()
    {
        var targetDirection = (PlayerTransform.position - transform.position).normalized;
        Rigidbody.velocity = targetDirection * movementSpeed;

        if (Time.time > NextAttackTime &&
            Vector3.Distance(PlayerTransform.position, transform.position) <= attackDistance)
            _currentEnemyState = EnemyState.Attacking;
    }


    private void Attacking()
    {
        if (!IsAttacking)
        {
            AttackTimeLeft = attackTime;
            var targetDirection = (PlayerTransform.position - transform.position).normalized;
            Rigidbody.velocity = targetDirection * (movementSpeed * attackMovementSpeedMultiplier);
            IsAttacking = true;
        }

        AttackTimeLeft -= Time.fixedDeltaTime;
        if (AttackTimeLeft > 0) return;

        NextAttackTime = Time.time + attackRate;
        IsAttacking = false;
        _currentEnemyState = EnemyState.Sleeping;
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player component)) component.TakeDamage(damage);
    }

    protected override void Die()
    {
        EnemyDeath.Invoke();
        base.Die();
    }

    private enum EnemyState
    {
        Sleeping,
        Chasing,
        Attacking
    }

}