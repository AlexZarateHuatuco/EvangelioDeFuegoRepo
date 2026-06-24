using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    int Damage = 10;
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