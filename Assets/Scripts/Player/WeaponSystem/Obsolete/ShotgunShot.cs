/*
using UnityEngine;
using System.Collections;

public class ShotgunShot : WeaponBase
{
    [Header("Referencias")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Disparo")]
    public int pellets = 8;
    public float spread = 5f;

    [Header("Bala")]
    public float bulletSpeed = 40f;

    [Header("Recarga")]
    public float reloadTime = 1.5f;

    private bool isReloading = false;
    private float fireCooldown = 0f;
    private Coroutine reloadCoroutine;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Shotgun;
        bulletType = BulletType.ShotgunBullet;

        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                Debug.LogError("[ShotgunShot] No se encontró una cámara principal.");
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

        if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    public override void Shoot()
    {
        currentAmmo--;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 baseDir = ray.direction;

        for (int i = 0; i < pellets; i++)
        {
            Vector2 spreadVec = Random.insideUnitCircle * spread;
            Vector3 dir = Quaternion.Euler(spreadVec.y, spreadVec.x, 0f) * baseDir;

            SpawnBullet(firePoint.position, Quaternion.LookRotation(dir), dir.normalized);
        }
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
            Debug.LogWarning("[ShotgunShot] El prefab de bala no tiene BulletData.");
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

public class ShotgunShot : WeaponBase
{
    [Header("Referencias")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Disparo")]
    public int pellets = 8;
    public float spread = 5f;

    [Header("Bala")]
    public float bulletSpeed = 40f;

    [Header("Recarga")]
    public float reloadTime = 1.5f;

    private bool isReloading = false;
    private float fireCooldown = 0f;
    private Coroutine reloadCoroutine;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Shotgun;
        bulletType = BulletType.Ice;

        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                Debug.LogError("[ShotgunShot] No se encontró una cámara principal.");
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

        if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    public override void Shoot()
    {
        currentAmmo--;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 baseDir = ray.direction;

        for (int i = 0; i < pellets; i++)
        {
            Vector2 spreadVec = Random.insideUnitCircle * spread;
            Vector3 dir = Quaternion.Euler(spreadVec.y, spreadVec.x, 0f) * baseDir;

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.LookRotation(dir)
            );

            SetupBullet(bullet);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = dir.normalized * bulletSpeed;
        }
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