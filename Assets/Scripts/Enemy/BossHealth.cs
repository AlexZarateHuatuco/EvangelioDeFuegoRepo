using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    // Events
    public event Action OnBossDeath;
    public event Action OnBossDamaged;

    [SerializeField] private bool isBoss = false;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentArmor;
    [SerializeField] private float maxArmor = 10f;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float CurrentArmor
    {
        get { return currentArmor; }
        set { currentArmor = value; }
    }

    public float MaxArmor
    {
        get { return maxArmor; }
        set { maxArmor = value; }
    }

    public bool IsBoss
    {
        get { return isBoss; }
        set { isBoss = value; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletRifle") || other.CompareTag("BulletShotgun") || other.CompareTag("Grenade"))
        {
            BulletRifle bulletRifle = other.GetComponent<BulletRifle>();
            BulletShotgun bulletShotgun = other.GetComponent<BulletShotgun>();
            if (bulletRifle != null)
            {
                TakeDamage(bulletRifle.damage);
            }
            if (bulletShotgun != null)
            {
                TakeDamage(bulletShotgun.damage);
            }

            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log($"[{gameObject.name}] Vida antes: {currentArmor}");

        currentArmor -= damage;

        currentArmor = Mathf.Clamp(currentArmor, 0f, maxArmor);

        //Debug.Log($"[{gameObject.name}] Daño recibido: {damage}");
        //Debug.Log($"[{gameObject.name}] Vida restante: {currentArmor}");

        OnBossDamaged?.Invoke();

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.BossDamaged();
        }

        if (currentArmor <= 0f)
        {
            //Debug.Log($"[{gameObject.name}] Vida antes: {currentHealth}");

            currentHealth -= damage;

            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            //Debug.Log($"[{gameObject.name}] Daño recibido: {damage}");
            //Debug.Log($"[{gameObject.name}] Vida restante: {currentHealth}");

            OnBossDamaged?.Invoke();

            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.BossDamaged();
            }

            if (currentHealth <= 0f)
            {
                Die();
            }
        }
        
    }

    public void Die()
    {
        //Debug.Log($"[{gameObject.name}] Boss died.");

        OnBossDeath?.Invoke();

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.BossKilled();
        }

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}