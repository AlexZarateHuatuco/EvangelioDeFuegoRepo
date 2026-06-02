using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public EnemySpawner spawner;

    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            spawner.StartSpawner();
        }
    }
}