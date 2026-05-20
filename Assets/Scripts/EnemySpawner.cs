using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    /*[Header("Referencias")]
    public Transform player;
    public GameObject enemyPrefab;

    [Header("Spawn")]
    public float spawnRadius = 10f;
    public LayerMask groundLayer;

    [Header("Oleadas")]
    public int enemiesPerWave = 5;
    public int enemiesIncreasePerWave = 2;

    [Header("Tiempos")]
    public float normalSpawnDelay = 0.5f;
    public float restSpawnDelay = 4f;
    public float timeBetweenWaves = 7f;

    [Header("Descanso")]
    public int enemiesDuringRest = 2;

    [Header("Separación")]
    public float minimumDistanceBetweenEnemies = 4f;

    private int currentWave = 1;

    void Start()
    {
        StartCoroutine(WaveSystem());
    }

    IEnumerator WaveSystem()
    {
        while (true)
        {
            // -------------------------
            // OLEADA PRINCIPAL
            // -------------------------
            int enemiesToSpawn =
                enemiesPerWave + ((currentWave - 1) * enemiesIncreasePerWave);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();

                yield return new WaitForSeconds(normalSpawnDelay);
            }

            Debug.Log("Oleada " + currentWave + " completada");

            // -------------------------
            // DESCANSO ENTRE OLEADAS
            // -------------------------
            float restTimer = 0f;
            int spawnedDuringRest = 0;

            while (restTimer < timeBetweenWaves)
            {
                if (spawnedDuringRest < enemiesDuringRest)
                {
                    SpawnEnemy();

                    spawnedDuringRest++;

                    yield return new WaitForSeconds(restSpawnDelay);

                    restTimer += restSpawnDelay;
                }
                else
                {
                    restTimer += Time.deltaTime;

                    yield return null;
                }
            }

            currentWave++;
        }
    }

    void SpawnEnemy()
    {
        const int maxAttempts = 30;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized;

            Vector3 candidatePosition =
                player.position +
                new Vector3(randomCircle.x, 0f, randomCircle.y) * spawnRadius;

            Ray ray = new Ray(
                candidatePosition + Vector3.up * 50f,
                Vector3.down
            );

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
            {
                Vector3 finalSpawnPos = hit.point;

                Collider[] nearbyEnemies = Physics.OverlapSphere(
                    finalSpawnPos,
                    minimumDistanceBetweenEnemies
                );

                bool tooClose = false;

                foreach (Collider col in nearbyEnemies)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    Instantiate(enemyPrefab, finalSpawnPos, Quaternion.identity);
                    return;
                }
            }
        }

        Debug.LogWarning("No se encontró una posición válida para generar enemigos.");
    }*/
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    private int currentIndex;

    public void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Generate();
        }
    }

    private void Generate()
    {
        Transform point =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(
            enemyPrefabs[currentIndex % enemyPrefabs.Count],
            point.position,
            Quaternion.identity);

        currentIndex++;
    }
}