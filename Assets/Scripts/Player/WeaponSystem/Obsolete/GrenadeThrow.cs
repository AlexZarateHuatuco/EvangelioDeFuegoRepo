/*
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
*/
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
        weaponType = WeaponType.Grenade;

        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.G))
            Shoot();

        if (Input.GetKeyDown(KeyCode.H))
            StartCoroutine(Reload());
    }

    public override void Shoot()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;

        GameObject grenade = Instantiate(
            grenadePrefab,
            throwPoint.position,
            playerCamera.transform.rotation
        );

        SetupBullet(grenade);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = playerCamera.transform.forward * throwForce;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}