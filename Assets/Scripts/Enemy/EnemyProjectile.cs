using UnityEngine;

public class EnemyProjectile : ProjectileBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(Damage);
            }
        }

        Destroy(gameObject);
    }
}