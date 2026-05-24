using Unity.Cinemachine;
using UnityEngine;

public class Armor : Consumables
{
    [SerializeField] private float armorAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                if (player.CurrentArmor < player.MaxArmor)
                {
                    PlayerEffect(player);
                    Destroy(gameObject);
                }
            }
        }
    }

    public override void PlayerEffect(PlayerHealth player)
    {
        player.RecoverArmor(armorAmount);
    }
}
