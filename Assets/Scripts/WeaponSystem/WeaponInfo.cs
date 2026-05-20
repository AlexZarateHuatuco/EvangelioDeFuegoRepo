using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [Header("HUD")]
    public string weaponName;
    public Sprite weaponIcon;

    [Header("Weapon")]
    public WeaponBase weapon;
}