/*using UnityEngine;
using System.Collections;

public class GrenadeThrow : WeaponBase
{

    [Header("Disparo")]
    public Camera playerCamera;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;

    [Header("Recarga")]
    public float reloadTime = 2f;

    private bool isReloading;

    protected override void Start()
    {
        base.Start();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.G) && currentAmmo > 0)
        {
            Shoot();
        }

        //if (currentAmmo <= 0)
        //{
            //StartCoroutine(Reload());
        //}
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Reload());
        }
    }

    public override void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        currentAmmo--;

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0)
        );

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 direction =
            (targetPoint - firePoint.position).normalized;

        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.LookRotation(direction)
            );

        Rigidbody rb =
            bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =
                direction * bulletSpeed;
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

public class GrenadeThrow : WeaponBase
{
    [Header("Referencias")]
    public Camera playerCamera;
    public Transform throwPoint;
    public GameObject grenadePrefab;

    [Header("Lanzamiento")]
    public float throwForce = 25f;

    [Header("Recarga")]
    public float reloadTime = 2f;

    private bool isReloading;

    protected override void Start()
    {
        base.Start();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Reload());
        }
    }

    public override void Shoot()
    {
        if (currentAmmo <= 0)
            return;

        currentAmmo--;

        GameObject grenade = 
        Instantiate(
        grenadePrefab,
        throwPoint.position,
        playerCamera.transform.rotation
        );
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }
}