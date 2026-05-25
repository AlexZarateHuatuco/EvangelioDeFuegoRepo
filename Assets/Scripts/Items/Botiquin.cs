using Unity.Cinemachine;
using UnityEngine;

public class Botiquin : Consumables
{
    [SerializeField] private float healthAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                if (player.CurrentHealth < player.MaxHealth)
                {
                    PlayerEffect(player);
                    Destroy(gameObject);
                }
            }
        }
    }

    public override void PlayerEffect(PlayerHealth player)
    {
        player.RecoverHealth(healthAmount);
    }
}
