using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject turretPrefab;
    public GameObject meleePrefab;

    [Header("Floor Trigger (the big collider covering the room)")]
    public Collider floorCollider;          // <-- assign your floor trigger here

    [Header("Placement Settings")]
    public float turretWallProximity = 1.5f;
    public float meleeNearTurretRadius = 1.5f;

    public void SpawnEnemies(int floor, int roomIndex, int totalCombatRooms)
    {
        if (floorCollider == null)
        {
            Debug.LogError("EnemyGen: floorCollider not assigned!");
            return;
        }

        Bounds bounds = floorCollider.bounds;
        float floorY = bounds.max.y;   // top of the floor collider = correct standing height

        // ---------- NUCLEAR TEST TURRET ----------
        if (turretPrefab != null)
        {
            Vector3 testPos = bounds.center;
            testPos.y = floorY;
            Instantiate(turretPrefab, testPos, Quaternion.identity);
            Debug.Log($"Nuclear turret spawned at {testPos}");
        }
        else
        {
            Debug.LogError("Turret prefab is not assigned in EnemyGen!");
            return;
        }

        // ---------- TURRETS ----------
        int turretPlanned = floor + 1 + roomIndex;
        int turretCap = 3 * floor + 3;
        int turretCount = Mathf.Min(turretPlanned, turretCap);

        List<Vector3> turretPositions = new List<Vector3>();

        for (int i = 0; i < turretCount; i++)
        {
            if (Random.value < 0.5f)
                continue;   // 50% failure

            Vector3 randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                floorY,
                Random.Range(bounds.min.z, bounds.max.z)
            );

            Instantiate(turretPrefab, randomPoint, Quaternion.identity);
            turretPositions.Add(randomPoint);
        }

        // ---------- MELEE ----------
        int meleeCount = turretPlanned;
        int meleePlaced = 0;

        // Place next to existing turrets
        foreach (Vector3 turretPos in turretPositions)
        {
            if (meleePlaced >= meleeCount) break;

            Vector3 offset = Random.insideUnitSphere * meleeNearTurretRadius;
            offset.y = 0;
            Vector3 candidate = turretPos + offset;
            candidate.y = floorY;

            Instantiate(meleePrefab, candidate, Quaternion.identity);
            meleePlaced++;
        }

        // Remaining melee go to room centre
        Vector3 center = bounds.center;
        center.y = floorY;
        for (int i = meleePlaced; i < meleeCount; i++)
        {
            Instantiate(meleePrefab, center, Quaternion.identity);
        }
    }
}