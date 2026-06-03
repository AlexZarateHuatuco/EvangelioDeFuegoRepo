using UnityEngine;

public class RoomTriggerBoss : MonoBehaviour
{
    public BossHealth bossHealth;

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

        bossHealth.OnBossDeath += OpenRoom;
    }

    private void OpenRoom()
    {
        entranceBarrier.SetActive(false);
        exitBarrier.SetActive(false);

        bossHealth.OnBossDeath -= OpenRoom;
    }
}