using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 1.5f;

    private float nextAttackTime;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            if (player.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damageAmount);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}