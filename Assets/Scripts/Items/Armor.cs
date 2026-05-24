using Unity.Cinemachine;
using UnityEngine;

public class Armor : Consumables
{
    public PlayerHealth player;

    public Armor(PlayerHealth p) : base(p)
    {
        this.player = p;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHealth"))
        {
            if (player.currentArmor < player.maxArmor)
            {
                PlayerEffect(player);
            }
        }
    }

    public override void PlayerEffect(PlayerHealth player)
    {
        player.RecoverArmor(10);
        Destroy(other.gameObject);
    }
}
