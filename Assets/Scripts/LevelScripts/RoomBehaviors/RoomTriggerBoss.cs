using UnityEngine;
public class RoomTriggerBoss : MonoBehaviour
{
    private bool activated;
    public Collider roomZone;
    public GameObject exitBarrier;
    public GameObject entranceBarrier;
    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (!other.transform.root.CompareTag("Player"))
            return;

        Debug.Log("EL JUGADOR ENTRÓ (Boss)");

        activated = true;

        entranceBarrier.SetActive(true);
        exitBarrier.SetActive(true);

        var allEnemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        foreach (var enemy in allEnemies)
        {
            if (enemy.IsBoss && roomZone.bounds.Contains(enemy.transform.position))
            {
                enemy.OnEnemyDeath += OpenRoom;
                break;
            }
        }
    }

    private void OpenRoom()
    {
        entranceBarrier.SetActive(false);
        exitBarrier.SetActive(false);
    }
}