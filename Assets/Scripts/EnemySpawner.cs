using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab;
    }

    public List<EnemyType> enemyPrefabs = new();
    public Transform[] spawnPoints;

    public int[] enemiesPerWave;
    private int defaultEnemiesPerWave = 5;

    [SerializeField] private int totalWaves = 3;
    private float timeBetweenWaves = 5f;

    private int currentWave;
    private int aliveEnemies;
    private bool started;
    private bool isFinished;

    public bool IsFinished
    {
        get { return isFinished; }
        set { isFinished = value; }
    }

    public int TotalWaves
    {
        get { return totalWaves; }
        set { totalWaves = value; }
    }

    public float TimeBetweenWaves
    {
        get { return timeBetweenWaves; }
        set { timeBetweenWaves = value; }
    }

    public void StartSpawner()
    {
        if (started)
            return;

        started = true;
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        for (currentWave = 1; currentWave <= totalWaves; currentWave++)
        {
            WaveUI.Instance?.UpdateWave(currentWave, totalWaves);

            int amountToSpawn = defaultEnemiesPerWave;

            if (currentWave - 1 < enemiesPerWave.Length)
            {
                if (enemiesPerWave[currentWave - 1] > 0)
                    amountToSpawn = enemiesPerWave[currentWave - 1];
            }

            SpawnWave(amountToSpawn);

            while (aliveEnemies > 0)
            {
                WaveUI.Instance?.UpdateEnemies(aliveEnemies);
                yield return null;
            }

            WaveUI.Instance?.UpdateEnemies(0);

            if (currentWave < totalWaves)
            {
                float timer = timeBetweenWaves;

                while (timer > 0)
                {
                    WaveUI.Instance?.UpdateRest(timer);
                    timer -= Time.deltaTime;
                    yield return null;
                }

                WaveUI.Instance?.HideRest();
            }
        }

        IsFinished = true;
    }

    void SpawnWave(int amount)
    {
        aliveEnemies = amount;

        for (int i = 0; i < amount; i++)
        {
            Transform spawnPoint =
                spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemy =
                Instantiate(
                    enemyPrefabs[Random.Range(0, enemyPrefabs.Count)].prefab,
                    spawnPoint.position,
                    spawnPoint.rotation);

            EnemyCounter counter =
                enemy.GetComponent<EnemyCounter>();

            if (counter == null)
                counter = enemy.AddComponent<EnemyCounter>();

            counter.Initialize(this);
        }

        WaveUI.Instance?.UpdateEnemies(aliveEnemies);
    }

    public void EnemyKilled()
    {
        aliveEnemies--;

        if (aliveEnemies < 0)
            aliveEnemies = 0;

        WaveUI.Instance?.UpdateEnemies(aliveEnemies);
    }
}