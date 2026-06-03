using UnityEngine;

public class Granada : MonoBehaviour
{
    public float damage = 10f;
    public PlayerHealth player;
    public EnemyHealthRifle enemyRifle;
    public EnemyHealthShotgun enemyShotgun;

    public Granada(float dmg)
    {
        this.damage = dmg;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("EnemyRifle"))
        {
            enemyRifle.TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("EnemyShotgun"))
        {
            enemyShotgun.TakeDamage(damage);
        }
    }
}
