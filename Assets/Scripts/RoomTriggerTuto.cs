using UnityEngine;
public class RoomTriggerTuto : MonoBehaviour
{
    private bool activated;
    public Collider roomZone;
    private int remainingEnemies;
    public GameObject exitBarrier;
    public GameObject entranceBarrier;
        private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (!other.transform.root.CompareTag("Player"))
            return;

        Debug.Log("EL JUGADOR ENTRÓ (Tutorial)");

        activated = true;

        entranceBarrier.SetActive(true);
        exitBarrier.SetActive(true);

        var allEnemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        foreach (var enemy in allEnemies)
        {
            if (roomZone.bounds.Contains(enemy.transform.position))
            {
                remainingEnemies++;
                enemy.OnEnemyDeath += OnEnemyDied;
            }
        }

        if (remainingEnemies == 0)
            OpenRoom();
    }

    private void OnEnemyDied()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
            OpenRoom();
    }

    private void OpenRoom()
    {
        entranceBarrier.SetActive(false);
        exitBarrier.SetActive(false);
    }
}