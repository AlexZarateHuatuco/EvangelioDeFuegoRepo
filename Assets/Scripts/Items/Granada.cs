using UnityEngine;

public class Granada : MonoBehaviour
{
    public float damage = 10f;
    public PlayerHealth player;

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
    }
}
