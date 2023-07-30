using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private BulletTarget ignoreTarget;
    
    private Rigidbody _rigidbody;
    private int _damage;
    
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 direction, int damage)
    {
        _rigidbody.velocity = direction * speed;
        _damage = damage;
        //rotate
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


