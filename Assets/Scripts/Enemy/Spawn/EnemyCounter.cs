using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private EnemySpawner spawner;

    public void Initialize(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
        var health = GetComponent<EnemyHealth>();
        if (health != null)
            health.OnEnemyDeath += OnDeath;
    }

    private void OnDeath()
    {
        if (spawner != null)
            spawner.EnemyKilled();
    }
}