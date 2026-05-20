/*using UnityEngine;
using System.Collections;

public class RifleShot : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Disparo")]
    public float fireRate = 10f;

    [Header("Munición")]
    public int magazineSize = 30;
    public float reloadTime = 2f;

    [Header("Bala")]
    public float bulletSpeed = 50f;

    private int currentAmmo;
    float fireCooldown = 0f;
    private bool isReloading = false;

    void OnEnable()
    {
        isReloading = false;
    }

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }

        if (currentAmmo > 0 && isReloading)
        {
            isReloading = false;
        }

        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        currentAmmo--;

        //Dirección desde la cámara (centro pantalla)
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 direction = ray.direction;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        Rigidbody rb = bullet.GetComponentInChildren<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * bulletSpeed;
        }
        else
        {
            Debug.LogError("La bala no tiene Rigidbody");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        fireCooldown = 0f;
        isReloading = false;
    }

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => magazineSize;
}*/
using UnityEngine;
using System.Collections;

public class RifleShot : WeaponBase
{
    [Header("Referencias")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Disparo")]
    public float fireRate = 10f;

    [Header("Bala")]
    public float bulletSpeed = 50f;

    private float fireCooldown = 0f;
    private bool isReloading = false;

    protected override void Start()
    {
        base.Start();
    }

    void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (isReloading)
            return;

        //Recarga automática
        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }

        //Cooldown del disparo
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        //Disparo automático
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

        //Ray desde el centro de la cámara
        Ray ray = cam.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        Vector3 direction = ray.direction;

        //Crear bala
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        //Mover bala
        Rigidbody rb = bullet.GetComponentInChildren<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =
                direction.normalized * bulletSpeed;
        }
        else
        {
            Debug.LogError("La bala no tiene Rigidbody");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;

        fireCooldown = 0f;

        isReloading = false;
    }
}