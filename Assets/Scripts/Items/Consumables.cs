using UnityEngine;

public class Consumables : MonoBehaviour
{
    public PlayerHealth player;

    public Consumables(PlayerHealth p)
    {
        this.player = p;
    }

    public virtual void PlayerEffect(PlayerHealth player);
}
