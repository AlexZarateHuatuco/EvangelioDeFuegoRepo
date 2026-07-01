using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Collider roomTrigger;               // trigger that covers the room
    public GameObject backtrackDoor;           // starts DISABLED (open)
    public GameObject[] standardDoors;         // start DISABLED (open)

    private bool backtrackLocked = false;

    void Update()
    {
        // 1. Lock the backtrack door the first time a player enters
        if (!backtrackLocked && backtrackDoor && PlayerIsInside())
        {
            backtrackDoor.SetActive(true);
            backtrackLocked = true;
        }

        // 2. Standard doors: close only when BOTH a player and an enemy are inside
        bool playerInside = PlayerIsInside();
        bool enemyInside = EnemyIsInside();
        bool closeDoors = playerInside && enemyInside;

        foreach (GameObject d in standardDoors)
            if (d) d.SetActive(closeDoors);
    }

    private bool PlayerIsInside()
    {
        if (!roomTrigger) return false;
        Bounds b = roomTrigger.bounds;
        Collider[] hits = Physics.OverlapBox(b.center, b.extents, roomTrigger.transform.rotation);
        foreach (Collider c in hits)
            if (c.CompareTag("Player"))
                return true;
        return false;
    }

    private bool EnemyIsInside()
    {
        if (!roomTrigger) return false;
        Bounds b = roomTrigger.bounds;
        Collider[] hits = Physics.OverlapBox(b.center, b.extents, roomTrigger.transform.rotation);
        foreach (Collider c in hits)
            if (c.GetComponent<EnemyHealth>())
                return true;
        return false;
    }
}
