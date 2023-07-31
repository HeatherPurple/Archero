using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletTarget ignoreTarget = BulletTarget.Player;
    
    private Rigidbody _rigidbody;
    private int _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 direction, int damage, float speed = 10f)
    {
        _rigidbody.velocity = direction * speed;
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ignoreTarget.ToString())) return;
        
        if (other.gameObject.TryGetComponent(out Creature component)) component.TakeDamage(_damage);
        
        Destroy(gameObject);
    }

    private enum BulletTarget
    {
        Player,
        Enemy
    }
}


