using UnityEngine;
using System.Collections;

public class ShotgunShot : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Disparo")]
    public int pellets = 8;
    public float spread = 5f;
    public float fireRate = 1f;

    [Header("Munición")]
    public int magazineSize = 8;
    public float reloadTime = 2f;

    [Header("Bala")]
    public float bulletSpeed = 40f;

    private int currentAmmo;
    private bool isReloading = false;
    float fireCooldown = 0f;

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

        if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        currentAmmo--;

        //Dirección base desde cámara
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 baseDirection = ray.direction;

        for (int i = 0; i < pellets; i++)
        {
            //Spread circular real
            Vector2 randomCircle = Random.insideUnitCircle * spread;

            Vector3 direction = Quaternion.Euler(
                randomCircle.y,
                randomCircle.x,
                0f
            ) * baseDirection;

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