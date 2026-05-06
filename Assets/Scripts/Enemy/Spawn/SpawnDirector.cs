using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SpawnDirector : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject enemyPrefab;

    [Header("Wave Settings")]
    public int baseEnemies = 5;
    public float difficultyMultiplier = 1.2f;

    [Header("Rest Settings")]
    public float restTime = 21f;
    public int maxRestEnemies = 3;

    [Header("Spawn Distance")]
    public float minSpawnRadius = 12f;
    public float maxSpawnRadius = 20f;
    public float restExtraDistance = 5f;
    public float minSpawnDistance = 10f;

    [Header("Height Settings")]
    public float groundOffset = 1f;

    private int currentWave = 1;
    private int enemiesAlive = 0;
    private int enemiesSpawnedThisWave = 0;

    private bool isResting = false;
    private float restTimer = 0f;

    private SafeZoneDetector safeZone;

    void Start()
    {
        safeZone = GetComponent<SafeZoneDetector>();
        StartWave();
    }

    void Update()
    {
        FollowPlayer();

        if (safeZone != null && safeZone.IsInSafeZone)
            return;

        if (!isResting)
            HandleWave();
        else
            HandleRest();
    }

    void FollowPlayer()
    {
        if (player != null)
            transform.position = player.position;
    }

    void StartWave()
    {
        isResting = false;
        enemiesSpawnedThisWave = 0;

        int enemiesToSpawn = Mathf.RoundToInt(
            baseEnemies * Mathf.Pow(difficultyMultiplier, currentWave)
        );

        StartCoroutine(SpawnWave(enemiesToSpawn));
    }

    IEnumerator SpawnWave(int total)
    {
        while (enemiesSpawnedThisWave < total)
        {
            SpawnEnemy(false);
            enemiesSpawnedThisWave++;
            enemiesAlive++;

            yield return new WaitForSeconds(1f);
        }
    }

    void HandleWave()
    {
        if (enemiesAlive <= 0 && enemiesSpawnedThisWave > 0)
        {
            StartRest();
        }
    }

    void StartRest()
    {
        isResting = true;
        restTimer = restTime;
    }

    void HandleRest()
    {
        restTimer -= Time.deltaTime;

        if (Random.value < 0.02f)
        {
            if (enemiesAlive < maxRestEnemies)
            {
                SpawnEnemy(true);
                enemiesAlive++;
            }
        }

        if (restTimer <= 0f)
        {
            currentWave++;
            StartWave();
        }
    }

    void SpawnEnemy(bool isRest)
    {
        Vector3 spawnPos = GetSpawnPosition(isRest);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetSpawnPosition(bool isRest)
    {
        int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 dir = Random.insideUnitSphere;
            dir.y = 0;
            dir.Normalize();

            float distance = Random.Range(minSpawnRadius, maxSpawnRadius);

            if (isRest)
                distance += restExtraDistance;

            Vector3 candidate = transform.position + dir * distance;

            if (Vector3.Distance(candidate, player.position) < minSpawnDistance)
                continue;

            NavMeshHit navHit;
            if (NavMesh.SamplePosition(candidate, out navHit, 5f, NavMesh.AllAreas))
            {
                Vector3 navPos = navHit.position;

                RaycastHit groundHit;
                if (Physics.Raycast(navPos + Vector3.up * 10f, Vector3.down, out groundHit, 50f))
                {
                    Vector3 finalPos = groundHit.point + Vector3.up * groundOffset;

                    if (Camera.main != null)
                    {
                        Vector3 vp = Camera.main.WorldToViewportPoint(finalPos);

                        if (vp.x > 0 && vp.x < 1 &&
                            vp.y > 0 && vp.y < 1 &&
                            vp.z > 0)
                            continue;
                    }

                    return finalPos;
                }
            }
        }

        return player.position + (Random.onUnitSphere * minSpawnRadius) + Vector3.up * groundOffset;
    }

    public void OnEnemyDeath()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }
}