using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Identificación")]
    public string weaponName = "Pistol";
    public Sprite weaponIcon;

    [Header("Tipo de bala")]
    //public BulletType bulletType;

    [Header("Estadísticas de disparo")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float range = 50f;

    [Header("Munición")]
    public int maxAmmoInClip = 30;
    public int maxAmmoReserve = 120;

    [Header("Recarga")]
    public float reloadTime = 1.5f;

    [Header("Prefabs")]
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Fire Mode")]
    public FireMode fireMode = FireMode.Single;

    [Header("Shotgun")]
    public int pelletsCount = 8;
    [Range(0f, 45f)]
    public float spreadAngle = 15f;

    [Header("Burst")]
    public int burstCount = 3;
    public float burstFireRate = 0.08f;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;
}
public enum FireMode
{
    Single,
    Shotgun,
    Burst
}