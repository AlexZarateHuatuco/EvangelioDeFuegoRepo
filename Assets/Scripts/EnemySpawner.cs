using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public GameObject enemyPrefab;

    [Header("Spawn")]
    public float spawnRadius = 10f;

    [Header("Oleadas")]
    public int enemiesPerWave = 5;
    public int enemiesIncreasePerWave = 2;

    [Header("Tiempos")]
    public float normalSpawnDelay = 0.5f;
    public float restSpawnDelay = 4f;
    public float timeBetweenWaves = 7f;

    [Header("Descanso")]
    public int enemiesDuringRest = 2;

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

                // Spawn rápido durante la oleada
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
                // Spawn lento durante el descanso
                if (spawnedDuringRest < enemiesDuringRest)
                {
                    SpawnEnemy();
                    spawnedDuringRest++;

                    yield return new WaitForSeconds(restSpawnDelay);
                    restTimer += restSpawnDelay;
                }
                else
                {
                    yield return null;
                    restTimer += Time.deltaTime;
                }
            }

            currentWave++;
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized;

        Vector3 spawnPos = player.position +
                           new Vector3(randomCircle.x, 0, randomCircle.y) * spawnRadius;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}