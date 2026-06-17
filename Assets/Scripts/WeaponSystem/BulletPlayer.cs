/*
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 40f;

    [Header("Lifetime")]
    public float lifeTime = 2f;

    [HideInInspector] public float damage;
    [HideInInspector] public BulletType bulletType;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletPlayer : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float bulletSpeed = 0f;

    private float damage;
    private float range;

    private Vector3 startPosition;
    private Rigidbody rb;

    public BulletType _bulletType
    {
        get { return bulletType; }
        set { bulletType = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    public void Launch(Vector3 direction)
    {
        rb.linearVelocity = direction.normalized * bulletSpeed;
    }

    private void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Wall"))
        {
            // Agregar efectos de impacto.
            Destroy(gameObject);
        }
    }
}

public enum BulletType
{
    Normal,
    Fire,
    Ice,
    Electric,
    Explosive,
    Poison,
    Light,
    Dark,
    Grenade
}