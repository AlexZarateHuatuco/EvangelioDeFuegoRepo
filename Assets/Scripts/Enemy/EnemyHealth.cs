using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Events
    public event Action OnEnemyDeath;
    public event Action OnEnemyDamaged;
    
    //Variables
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float debugDamage = 2f;
    [SerializeField] private float currentHealth = 3f;

    void Start()
    {
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
    }
}