/*
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
*/
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    ///[HideInInspector] public BulletType bulletType = BulletType.Grenade;
    ///[HideInInspector] public BulletType bulletType = BulletType.Grenade;
    [HideInInspector] public float damage = 80f;

    [Header("Explosión")]
    [SerializeField] private float fuseTime = 3f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject explosionEffectPrefab;

    private bool hasExploded = false;

    private void Start()
    {
        StartCoroutine(FuseCountdown());
    }

    private IEnumerator FuseCountdown()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.isTrigger) return;
        if (other.CompareTag("Enemy")) return;
        Explode();
    }

    private void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        if (explosionEffectPrefab != null)
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}