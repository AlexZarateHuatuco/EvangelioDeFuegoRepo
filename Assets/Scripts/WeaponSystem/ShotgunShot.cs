/*using UnityEngine;
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

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Shotgun;
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0)
            StartCoroutine(Reload());

        fireCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") &&
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
        Vector3 baseDir = ray.direction;

        for (int i = 0; i < pellets; i++)
        {
            Vector2 spreadVec = Random.insideUnitCircle * spread;

            Vector3 dir = Quaternion.Euler(
                spreadVec.y,
                spreadVec.x,
                0f
            ) * baseDir;

            if (Physics.Raycast(firePoint.position, dir, out RaycastHit hit, 100f))
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
                Quaternion.LookRotation(dir)
            );

            Rigidbody rb = bullet.GetComponentInChildren<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = dir.normalized * bulletSpeed;
        }
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

    protected override void Start()
    {
        base.Start();

        weaponType = WeaponType.Shotgun;

        if (cam == null)
        {
            cam = Camera.main;

            if (cam == null)
            {
                Debug.LogError("Shotgun: No se encontró una cámara principal.");
            }
        }
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }

        fireCooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") &&
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

        Vector3 baseDir = ray.direction;

        for (int i = 0; i < pellets; i++)
        {
            Vector2 spreadVec =
                Random.insideUnitCircle * spread;

            Vector3 dir = Quaternion.Euler(
                spreadVec.y,
                spreadVec.x,
                0f
            ) * baseDir;

            if (Physics.Raycast(
                firePoint.position,
                dir,
                out RaycastHit hit,
                100f))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    EnemyHealth enemy =
                        hit.collider.GetComponent<EnemyHealth>();

                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.LookRotation(dir)
            );

            Rigidbody rb =
                bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity =
                    dir.normalized * bulletSpeed;
            }
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