using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public EnemySpawner spawner;
    public GameObject entranceBarrier;
    public GameObject exitBarrier;

    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (!other.transform.root.CompareTag("Player"))
            return;

        Debug.Log("EL JUGADOR ENTRÓ");

        activated = true;

        entranceBarrier.SetActive(true);
        exitBarrier.SetActive(true);

        spawner.OnSpawnerFinished += OpenRoom;
        spawner.StartSpawner();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Nombre: " + other.name);
    //    Debug.Log("Tag: " + other.tag);

    //    if (activated)
    //        return;

    //    if (!other.CompareTag("Player"))
    //    {
    //        activated = true;

    //        entranceBarrier.SetActive(true);
    //        exitBarrier.SetActive(true);

    //        spawner.OnSpawnerFinished += OpenRoom;

    //        spawner.StartSpawner();
    //    }
    //}

    private void OpenRoom()
    {
        entranceBarrier.SetActive(false);
        exitBarrier.SetActive(false);

        spawner.OnSpawnerFinished -= OpenRoom;
    }
}