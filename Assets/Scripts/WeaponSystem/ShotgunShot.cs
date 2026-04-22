using UnityEngine;
using System.Collections;

public class ShotgunShot : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Settings")]
    public float fireRate = 1f;
    public int magazineSize = 8;
    public float reloadTime = 3f;
    public float bulletSpeed = 1f;

    [Header("Shotgun")]
    public int pellets = 8;
    public float spread = 5f;

    private int currentAmmo;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        /*currentAmmo--;

        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = firePoint.forward;

            direction = Quaternion.Euler(
                Random.Range(-spread, spread),
                Random.Range(-spread, spread),
                0
            ) * direction;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            rb.linearVelocity = direction.normalized * bulletSpeed;
        }*/
        currentAmmo--;

        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = firePoint.forward;

            direction = Quaternion.Euler(
                Random.Range(-spread, spread),
                Random.Range(-spread, spread),
                0
            ) * direction;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            rb.linearVelocity = direction.normalized * bulletSpeed;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        isReloading = false;
    }
}