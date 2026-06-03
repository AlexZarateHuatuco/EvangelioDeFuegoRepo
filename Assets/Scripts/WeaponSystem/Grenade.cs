/*using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 25f;

    [Header("Explosión")]
    public float explosionDelay = 2f;
    public float explosionRadius = 5f;
    public float damage = 75f;

    //public GameObject explosionEffect;

    private bool exploded = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity =
                transform.forward * speed;
        }

        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode()
    {
        if (exploded)
            return;

        exploded = true;

        //if (explosionEffect != null)
        //{
        //    Instantiate(
        //        explosionEffect,
        //        transform.position,
        //        Quaternion.identity
        //    );
        //}

        Collider[] hits =
            Physics.OverlapSphere(
                transform.position,
                explosionRadius
            );

        foreach (Collider hit in hits)
        {
            EnemyHealth enemy =
                hit.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}*/
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 25f;

    [Header("Explosión")]
    public float explosionDelay = 2f;
    public float explosionRadius = 5f;
    public float damage = 75f;

    private bool exploded = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;

            rb.linearVelocity =
                transform.forward * speed;
        }

        Invoke(nameof(Explode), explosionDelay);
    }

    void Explode()
    {
        if (exploded)
            return;

        exploded = true;

        Collider[] hits =
            Physics.OverlapSphere(
                transform.position,
                explosionRadius
            );

        foreach (Collider hit in hits)
        {
            EnemyHealthRifle enemyRifle =
                hit.GetComponent<EnemyHealthRifle>();
            EnemyHealthShotgun enemyShotgun =
                hit.GetComponent<EnemyHealthShotgun>();

            if (enemyRifle != null)
            {
                enemyRifle.TakeDamage(damage);
            }
            if (enemyShotgun != null)
            {
                enemyShotgun.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}