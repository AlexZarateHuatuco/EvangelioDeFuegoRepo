using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action OnEnemyDeath;
    public event Action OnEnemyDamaged;

    [SerializeField] private bool isMiniBoss;
    [SerializeField] private bool isBoss;

    [Header("Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [Header("Regeneration (bosses only)")]
    [SerializeField] private bool canRegenerate;
    [SerializeField] private float regenThresholdPercent;
    [SerializeField] private float regenAmountPerTick;
    [SerializeField] private float regenTickInterval;
    [SerializeField] private float regenDuration;

    public bool IsBoss => isBoss;
    public bool IsMiniBoss => isMiniBoss;

    private bool isRegenerating = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }


    private void OnTriggerEnter(Collider other)
    {
        Grenade grenade = other.GetComponent<Grenade>();
        if (grenade != null)
        {
            TakeDamage(grenade.damage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log($"[{gameObject.name}] Daño: {damage} | Vida restante: {currentHealth}");

        OnEnemyDamaged?.Invoke();

        if (TutorialManager.Instance != null)
            TutorialManager.Instance.EnemyDamaged();

        CheckRegenThreshold();

        if (currentHealth <= 0f)
            Die();
    }

    private void CheckRegenThreshold()
    {
        if (!canRegenerate || isRegenerating)
            return;

        float healthPercent = (currentHealth / maxHealth) * 100f;
        if (healthPercent <= regenThresholdPercent)
        {
            StartCoroutine(Regenerate());
        }
    }

    private IEnumerator Regenerate()
    {
        isRegenerating = true;

        Debug.Log($"[{gameObject.name}] Iniciando regeneración.");

        float elapsed = 0f;

        while (elapsed < regenDuration && currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + regenAmountPerTick, maxHealth);
            Debug.Log($"[{gameObject.name}] Regenerando vida: {currentHealth}");

            elapsed += regenTickInterval;
            yield return new WaitForSeconds(regenTickInterval);
        }

        isRegenerating = false;
        Debug.Log($"[{gameObject.name}] Regeneración terminada.");
    }

    public void Die()
    {
        Debug.Log($"[{gameObject.name}] Enemy died.");

        OnEnemyDeath?.Invoke();

        if (TutorialManager.Instance != null)
            TutorialManager.Instance.EnemyKilled();

        Destroy(gameObject);
    }

    public float GetHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
}