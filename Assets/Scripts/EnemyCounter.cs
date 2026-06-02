using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private EnemySpawner spawner;

    public void Initialize(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.EnemyKilled();
    }
}