/*using UnityEngine;
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

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Rifle;
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0 && !isReloading)
            StartCoroutine(Reload());

        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") &&
            fireCooldown <= 0f &&
            currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    public override void Shoot()
    {
        currentAmmo--;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                    enemy.TakeDamage(damage);
            }
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(ray.direction)
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = ray.direction.normalized * bulletSpeed;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}*/
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

    protected override void Start()
    {
        base.Start();

        weaponType = WeaponType.Rifle;

        if (cam == null)
        {
            cam = Camera.main;

            if (cam == null)
            {
                Debug.LogError("Rifle: No se encontró una cámara principal.");
            }
        }
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = MaxAmmo;
            isReloading = false;
        }

        fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") &&
            fireCooldown <= 0f &&
            currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    public override void Shoot()
    {
        currentAmmo--;

        Ray ray = cam.ViewportPointToRay(
            new Vector3(0.5f, 0.5f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.CompareTag("EnemyRifle"))
            {
                EnemyHealthRifle enemyRifle = hit.collider.GetComponent<EnemyHealthRifle>();

                if (enemyRifle != null)
                {
                    enemyRifle.TakeDamage(damage);
                }
            }
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(ray.direction)
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =
                ray.direction.normalized * bulletSpeed;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }
}