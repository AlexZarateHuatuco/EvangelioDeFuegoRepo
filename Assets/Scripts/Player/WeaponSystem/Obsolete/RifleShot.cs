/*
using UnityEngine;
using System.Collections;

public class RifleShot : WeaponBase
{
    [Header("Referencias")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Bala")]
    public float bulletSpeed = 50f;

    [Header("Recarga")]
    public float reloadTime = 1.5f;

    private float fireCooldown = 0f;
    private bool isReloading = false;
    private Coroutine reloadCoroutine;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Rifle;
        bulletType = BulletType.RifleBullet;

        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                Debug.LogError("[RifleShot] No se encontró una cámara principal.");
        }
    }

    void Update()
    {
        if (currentAmmo <= 0 && !isReloading)
            StartReload();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            StartReload();

        if (isReloading) return;

        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    public override void Shoot()
    {
        currentAmmo--;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        SpawnBullet(firePoint.position, Quaternion.LookRotation(ray.direction), ray.direction.normalized);
    }
    private void SpawnBullet(Vector3 position, Quaternion rotation, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);

        BulletData data = bullet.GetComponent<BulletData>();
        if (data != null)
        {
            data.bulletType = bulletType;
            data.damage = damage;
        }
        else
        {
            Debug.LogWarning("[RifleShot] El prefab de bala no tiene BulletData.");
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = direction * bulletSpeed;
    }

    private void StartReload()
    {
        if (reloadCoroutine != null)
            StopCoroutine(reloadCoroutine);

        reloadCoroutine = StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        reloadCoroutine = null;
    }

    private void OnDisable()
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            reloadCoroutine = null;
            isReloading = false;
        }
    }
}
*/
using UnityEngine;
using System.Collections;

public class RifleShot : WeaponBase
{
    [Header("Referencias")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Bala")]
    public float bulletSpeed = 50f;

    [Header("Recarga")]
    public float reloadTime = 1.5f;

    private float fireCooldown = 0f;
    private bool isReloading = false;
    private Coroutine reloadCoroutine;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Rifle;
        //bulletType = BulletType.Fire;

        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                Debug.LogError("[RifleShot] No se encontró una cámara principal.");
        }
    }

    void Update()
    {
        if (currentAmmo <= 0 && !isReloading)
            StartReload();

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            StartReload();

        if (isReloading) return;

        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    public override void Shoot()
    {
        currentAmmo--;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(ray.direction)
        );

        SetupBullet(bullet);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = ray.direction.normalized * bulletSpeed;
    }

    private void StartReload()
    {
        if (reloadCoroutine != null)
            StopCoroutine(reloadCoroutine);

        reloadCoroutine = StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        reloadCoroutine = null;
    }

    private void OnDisable()
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            reloadCoroutine = null;
            isReloading = false;
        }
    }
}