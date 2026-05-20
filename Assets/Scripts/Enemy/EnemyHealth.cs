/*using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //private SpawnDirector director;
    public WaveManager waveManager;
    //Events
    public event Action OnEnemyDeath;
    public event Action OnEnemyDamaged;
    
    //Variables
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float debugDamage = 2f;
    [SerializeField] private float currentHealth = 3f;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(debugDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        OnEnemyDamaged?.Invoke();
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy have died.");
        OnEnemyDeath?.Invoke();
        waveManager.EnemyKilled();
        Destroy(gameObject);
    }
}*/
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string bulletTag;
    [Header("Wave Manager")]
    public WaveManager waveManager;

    // Events
    public event Action OnEnemyDeath;
    public event Action OnEnemyDamaged;

    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;

    [Header("Boss")]
    public bool isBoss = false;

    private void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(bulletTag))
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"[{gameObject.name}] Vida antes: {currentHealth}");

        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log($"[{gameObject.name}] Daño recibido: {damage}");
        Debug.Log($"[{gameObject.name}] Vida restante: {currentHealth}");

        OnEnemyDamaged?.Invoke();

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.EnemyDamaged();
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"[{gameObject.name}] Enemy died.");

        OnEnemyDeath?.Invoke();

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.EnemyKilled();
        }

        if (waveManager != null)
        {
            waveManager.EnemyKilled();
        }

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}