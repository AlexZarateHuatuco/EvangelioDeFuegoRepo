using UnityEngine;

public class FloorRegen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public LongHallwayMaker generator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //generator.NextFloor();

            // Teleport player to the start of the new floor
            // Transform firstRoom = generator.GetFirstRoom();
            //if (firstRoom != null)
            //{
            //    Vector3 spawnPos = firstRoom.position;
            //    spawnPos.y += 1.5f;   // adjust to player height
            //    other.transform.position = spawnPos;
            //}
        }
    }
}


