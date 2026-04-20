using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("Life")]
    public float maxHealth = 100f;
    private float currentHealth;

    //[Header("Efectos")]
    //public GameObject hitEffect;
    //public GameObject deathEffect;

    [Header("Optional")]
    public bool destroyOnDeath = true;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        currentHealth -= damage;

        //if (hitEffect != null)
        //{
        //    Instantiate(hitEffect, hitPoint, Quaternion.LookRotation(hitNormal));
        //}

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        //if (deathEffect != null)
        //{
        //    Instantiate(deathEffect, transform.position, Quaternion.identity);
        //}

        Debug.Log(gameObject.name + " died");

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}