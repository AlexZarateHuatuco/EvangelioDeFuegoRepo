using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGen : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private GameObject meleePrefab;

    [Header("Placement Settings")]
    [SerializeField] private float turretWallProximity = 1.5f;
    [SerializeField] private float meleeNearTurretRadius = 1.5f;

    public void SpawnEnemies(int floor, int roomIndex, int totalCombatRooms)
    {
        BoxCollider boundsCollider = GetComponentInChildren<BoxCollider>();
        if (boundsCollider == null)
        {
            Debug.LogError("Combat room prefab missing a BoxCollider (trigger) for bounds!");
            return;
        }

        Bounds roomBounds = boundsCollider.bounds;

        // ---------- TURRETS ----------
        int turretPlanned = floor + 1 + roomIndex;
        int turretCap = 3 * floor + 3;
        int turretCount = Mathf.Min(turretPlanned, turretCap);

        List<Vector3> turretPositions = new List<Vector3>();

        for (int i = 0; i < turretCount; i++)
        {
            if (Random.value < 0.5f) continue;   // 50% failure per turret

            Vector3? pos = GetRandomNavMeshPoint(roomBounds, preferEdge: true);
            if (pos.HasValue)
            {
                Instantiate(turretPrefab, pos.Value, Quaternion.identity);
                turretPositions.Add(pos.Value);
            }
        }

        // ---------- MELEE ----------
        int meleeCount = turretPlanned;   // same planned count as turrets
        int meleePlaced = 0;

        // Try next to existing turrets first
        foreach (Vector3 turretPos in turretPositions)
        {
            if (meleePlaced >= meleeCount) break;

            Vector3 randomOffset = Random.insideUnitSphere * meleeNearTurretRadius;
            randomOffset.y = 0;
            Vector3 candidate = turretPos + randomOffset;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(candidate, out hit, 1.0f, NavMesh.AllAreas))
            {
                Instantiate(meleePrefab, hit.position, Quaternion.identity);
                meleePlaced++;
            }
        }

        // Remaining melee go to room centre
        Vector3 center = GetRoomCenter(roomBounds);
        for (int i = meleePlaced; i < meleeCount; i++)
        {
            Instantiate(meleePrefab, center, Quaternion.identity);
        }
    }

    private Vector3? GetRandomNavMeshPoint(Bounds bounds, bool preferEdge)
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                bounds.center.y,
                Random.Range(bounds.min.z, bounds.max.z)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                if (preferEdge)
                {
                    NavMeshHit edgeHit;
                    if (NavMesh.FindClosestEdge(hit.position, out edgeHit, NavMesh.AllAreas))
                    {
                        if (edgeHit.distance <= turretWallProximity)
                            return hit.position;
                    }
                }
                else
                {
                    return hit.position;
                }
            }
        }
        return null;
    }

    private Vector3 GetRoomCenter(Bounds bounds)
    {
        Vector3 center = bounds.center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(center, out hit, 2.0f, NavMesh.AllAreas))
            return hit.position;
        return center;
    }
}

