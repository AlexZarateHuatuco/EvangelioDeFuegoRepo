using UnityEngine;

public class EnemyKamikaze : MonoBehaviour
{
    [SerializeField] private float explosionDamage = 30f;
    [SerializeField] private float explosionRadius = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Explode();
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                if (hit.TryGetComponent(out PlayerHealth playerHealth))
                    playerHealth.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        //...i lov u gizmos. i am so zorry i gav u flak :x
    }
}