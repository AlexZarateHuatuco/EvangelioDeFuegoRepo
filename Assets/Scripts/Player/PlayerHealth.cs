using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //References
    //private EnemyHealth eh;

    //Events
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerDamaged;

    //Variables
    [SerializeField] private float currentHealth = 3f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float debugDamage = 2f;

    //Public variables
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

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        OnPlayerDamaged?.Invoke();
        Debug.Log($"Player have recieved ({damage}). Current Health: {currentHealth}");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        OnPlayerDamaged?.Invoke();
    }

    private void Die()
    {
        Debug.Log("Player have died.");
        OnPlayerDeath?.Invoke();
        SceneManager.LoadScene("DeathScreen");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si entramos en un hitbox enemigo
        //if (other.CompareTag("Enemy"))
        //{
        //    HitboxDamage hit = other.GetComponent<HitboxDamage>();

        //    if (hit != null)
        //    {
        //        Debug.Log($"Enemy '{other.name}' dealt: {hit.damageAmount}");
        //        TakeDamage(hit.damageAmount);
        //    }
        //    else
        //    {
        //        Debug.LogWarning($"Enemy '{other.name}' doesn't have HitboxDamage.");
        //    }
        //}

        //Perder vida sin script de enemigo
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(debugDamage);
        }

        //Recuperar vida por objetos
        //if (other.gameObject.CompareTag("Medkit"))
        //{
        //    Medkit medkit = other.GetComponent<Medkit>();
        //    if (medkit != null)
        //    {
        //        currentHealth += medkit.Recovery;
        //    }
        //}
    }
}