/*
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

    [Header("Bullet Type")]
    public BulletType bulletType;

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
*/
using UnityEngine;
public enum WeaponType
{
    Shotgun,
    Rifle,
    Grenade
}
public enum Bullet2Type
{
    RifleBullet,
    ShotgunBullet,
    GrenadeAmmo
}
public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Info")]
    public string weaponName;

    [Header("Damage")]
    public float damage = 25f;

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Bullet Type")]
    public BulletType bulletType;

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

    protected void SetupBullet(GameObject bulletObject)
    {
        BulletPlayer bullet = bulletObject.GetComponent<BulletPlayer>();
        if (bullet != null)
        {
            bullet.Damage = damage;
            bullet._bulletType = bulletType;
        }
        else
        {
            Debug.LogWarning($"{weaponName}: el prefab de bala no tiene BulletPlayer.");
        }
    }
}