/*using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;

    private Transform[] spawnPoints;
    private int currentIndex;

    private void Awake()
    {
        GameObject[] spawners =
            GameObject.FindGameObjectsWithTag("EnemySpawner");

        spawnPoints = new Transform[spawners.Length];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawnPoints[i] = spawners[i].transform;
        }

        Debug.Log("Spawners encontrados: " + spawnPoints.Length);
    }

    public void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Generate();
        }
    }

    private void Generate()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No se encontraron objetos con tag EnemySpawner");
            return;
        }

        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No hay enemigos asignados");
            return;
        }

        Transform point =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(
            enemyPrefabs[currentIndex % enemyPrefabs.Count],
            point.position,
            Quaternion.identity);

        currentIndex++;
    }
}*/
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;

    private Transform[] spawnPoints;
    private int currentIndex;

    private void Awake()
    {
        GameObject[] spawners =
            GameObject.FindGameObjectsWithTag("EnemySpawner");

        spawnPoints = new Transform[spawners.Length];

        for (int i = 0; i < spawners.Length; i++)
        {
            spawnPoints[i] = spawners[i].transform;
        }

        Debug.Log("Spawners encontrados: " + spawnPoints.Length);
    }

    public void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Generate();
        }
    }

    private void Generate()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No se encontraron objetos con tag EnemySpawner");
            return;
        }

        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No hay enemigos asignados");
            return;
        }

        Transform point =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        NavMeshHit hit;

        if (NavMesh.SamplePosition(
            point.position,
            out hit,
            1f,
            NavMesh.AllAreas))
        {
            GameObject enemy = Instantiate(
                enemyPrefabs[currentIndex % enemyPrefabs.Count],
                hit.position,
                Quaternion.identity);

            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

            if (agent != null)
            {
                Debug.Log(
                    $"{enemy.name} | OnNavMesh: {agent.isOnNavMesh}");
            }

            currentIndex++;
        }
        else
        {
            Debug.LogWarning(
                $"No se encontró NavMesh cerca de {point.name}");
        }
    }
}