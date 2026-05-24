using Unity.Cinemachine;
using UnityEngine;

public class Botiquin : Consumables
{
    public PlayerHealth player;

    public Botiquin(PlayerHealth p) : base(p)
    {
        this.player = p;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHealth"))
        {
            if(player.CurrentHealth < player.MaxHealth)
            {
                PlayerEffect(player);
            }
        }
    }

    public override void PlayerEffect(PlayerHealth player)
    {
        player.Heal(10);
        Destroy(other.gameObject);
    }
}
