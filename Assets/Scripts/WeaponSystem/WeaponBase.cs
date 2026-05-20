using UnityEngine;

public enum WeaponType
{
    Shotgun,
    Rifle,
    Grenade
}

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Info")]
    public string weaponName;

    [Header("Damage")]
    public float damage = 25f;

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Ammo")]
    public int currentAmmo = 30;
    public int maxAmmo = 30;

    [Header("Fire")]
    public float fireRate = 10f;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
    }

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => maxAmmo;

    public abstract void Shoot();
}