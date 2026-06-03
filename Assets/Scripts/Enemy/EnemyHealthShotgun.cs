using System;
using UnityEngine;

public class EnemyHealthShotgun : MonoBehaviour
{

    // Events
    public event Action OnEnemyDeath;
    public event Action OnEnemyDamaged;

    [SerializeField] private string bulletTag;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 10f;

    public bool IsBoss
    {
        get { return isBoss; }
        set { isBoss = value; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletShotgun"))
        {
            BulletShotgun bullet = other.GetComponent<BulletShotgun>();

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

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}