/*
using UnityEngine;

public class BulletShotgun : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 40f;

    [Header("Damage")]
    public float damage = 10f;

    [Header("Weapon Reference")]
    public WeaponBase weapon;

    [Header("Lifetime")]
    public float lifeTime = 2f;

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
        EnemyHealthShotgun enemyShotgun = other.GetComponent<EnemyHealthShotgun>();

        if (enemyShotgun != null)
        {
            enemyShotgun.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("EnemyShotgun")  || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
*/
using UnityEngine;

public class BulletShotgun : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 40f;

    [Header("Damage")]
    public float damage = 10f;

    [Header("Weapon Reference")]
    public WeaponBase weapon;

    [Header("Lifetime")]
    public float lifeTime = 2f;

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

        EnemyHealthShotgun enemyShotgun = other.GetComponent<EnemyHealthShotgun>();
        if (enemyShotgun != null)
        {
            enemyShotgun.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}