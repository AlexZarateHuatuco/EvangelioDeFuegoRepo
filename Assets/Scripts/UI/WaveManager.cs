/*using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int currentWave = 0;

    public int baseEnemies = 5;
    public float timeBetweenWaves = 5f;

    private int enemiesAlive;

    [Header("References")]
    public EnemySpawner spawner;

    [Header("UI")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI countdownText;

    private bool waveInProgress = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        waveInProgress = true;

        float timer = timeBetweenWaves;

        while (timer > 0)
        {
            countdownText.text = "Next Wave In: " + Mathf.Ceil(timer);
            timer -= Time.deltaTime;
            yield return null;
        }

        countdownText.text = "";

        currentWave++;

        int enemyCount = baseEnemies + (currentWave * 2);

        enemiesAlive = enemyCount;

        UpdateUI();

        spawner.SpawnEnemies(enemyCount);

        waveInProgress = false;
    }

    public void EnemyKilled()
    {
        enemiesAlive--;

        UpdateUI();

        if (enemiesAlive <= 0 && !waveInProgress)
        {
            StartCoroutine(StartNextWave());
        }
    }

    void UpdateUI()
    {
        waveText.text = "Wave " + currentWave;
        enemiesText.text = "Enemies Left: " + enemiesAlive;
    }
}*/
using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int currentWave = 0;

    public int baseEnemies = 5;
    public float timeBetweenWaves = 5f;

    private int enemiesAlive;
    private bool waveInProgress = false;

    [Header("References")]
    [SerializeField] private EnemySpawnerManager spawner;

    [Header("UI")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI countdownText;

    private void Awake()
    {
        // Busca automáticamente el spawner en la escena
        spawner = FindFirstObjectByType<EnemySpawnerManager>();

        if (spawner == null)
        {
            Debug.LogError("No se encontró EnemySpawnerManager en la escena");
        }
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        waveInProgress = true;

        float timer = timeBetweenWaves;

        while (timer > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = "Next Wave In: " + Mathf.Ceil(timer);
            }

            timer -= Time.deltaTime;

            yield return null;
        }

        if (countdownText != null)
        {
            countdownText.text = "";
        }

        currentWave++;

        int enemyCount = baseEnemies + (currentWave * 2);

        enemiesAlive = enemyCount;

        UpdateUI();

        if (spawner != null)
        {
            spawner.SpawnEnemies(enemyCount);
        }

        waveInProgress = false;
    }

    public void EnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive < 0)
        {
            enemiesAlive = 0;
        }

        UpdateUI();

        if (enemiesAlive <= 0 && !waveInProgress)
        {
            StartCoroutine(StartNextWave());
        }
    }

    private void UpdateUI()
    {
        if (waveText != null)
        {
            waveText.text = "Wave " + currentWave;
        }

        if (enemiesText != null)
        {
            enemiesText.text = "Enemies Left: " + enemiesAlive;
        }
    }
}