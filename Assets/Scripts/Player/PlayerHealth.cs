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
    [SerializeField] private float currentArmor = 10f;
    [SerializeField] private float maxArmor = 10f;
    [SerializeField] private float currentHealth = 10f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float debugDamage = 1f;

    //Public variables
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
        currentArmor = maxArmor;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentArmor -= damage;
        currentArmor = Mathf.Clamp(currentArmor, 0f, maxArmor);
        Debug.Log($"Player have recieved ({damage}). Current Armor: {currentArmor}");
        if (currentArmor == 0)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            Debug.Log($"Player have recieved ({damage}). Current Health: {currentHealth}");
        }
        OnPlayerDamaged?.Invoke();
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void RecoverHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        OnPlayerDamaged?.Invoke();
    }

    public void RecoverArmor(float amount)
    {
        currentArmor = Mathf.Clamp(currentArmor + amount, 0f, maxArmor);
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
        //Perder vida sin script de enemigo
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(debugDamage);
        }

        //Recuperar vida por objetos
        //if (other.gameObject.CompareTag("Medkit"))
        {
            //Medkit medkit = other.GetComponent<Medkit>();
            //if (medkit != null)
            {
                //currentHealth += medkit.Recovery;
            }
        }
        //if (other.gameObject.CompareTag("Armor"))
        {
            //Armor armor = other.GetComponent<Armor>();
            //if (armor != null)
            {
                //currentArmor += armor.Recovery;
            }
        }
    }
}