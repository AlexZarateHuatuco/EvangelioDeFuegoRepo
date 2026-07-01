using UnityEngine;

public class WhiteBlockSimulacra : MonoBehaviour
{
    [SerializeField] private int floor = 1;
    [SerializeField] private int roomIndex = 0;
    [SerializeField] private int totalCombatRooms = 1;

    private void Start()
    {
        EnemyGen spawner = GetComponent<EnemyGen>();
        if (spawner != null)
            spawner.SpawnEnemies(floor, roomIndex, totalCombatRooms);
    }
}

