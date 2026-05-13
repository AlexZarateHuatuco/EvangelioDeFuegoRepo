using UnityEngine;
using System.Collections;

public class GrenadeLauncher : MonoBehaviour
{
    private int currentAmmo = 5;
    public float grenadeSpeed = 50f;
    float fireCooldown = 0f;
    private bool isReloading = false;

    [Header("Munición")]
    public int magazineSize = 30;
    public float reloadTime = 2f;

    [Header("Referencias")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject grenadePrefab;

    [Header("Configuración de lanzamiento")]
    [SerializeField] private float maxDistance = 50f;
    [SerializeField] private float arcHeight = 2.5f;

    [Header("Explosión")]
    [SerializeField] private float explosionRadius = 5f;

    void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;

            if (cam == null)
            {
                Debug.LogError("No se encontró una cámara con tag 'MainCamera'");
            }
        }
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            DebugShoot();
        }

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
            fireCooldown -= Time.deltaTime;*/
    }

    void DebugShoot()
    {
        currentAmmo--;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 direction = ray.direction;

        GameObject bullet = Instantiate(
            grenadePrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        Rigidbody rb = bullet.GetComponentInChildren<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * grenadeSpeed;
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

    void ThrowGrenade()
    {
        if (cam == null) return;

        Vector3 aimPoint = GetAimPoint();
        Vector3 adjustedPoint = AdjustPoint(firePoint.position, aimPoint, explosionRadius);

        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        Vector3 velocity = CalculateVelocity(firePoint.position, adjustedPoint, arcHeight);

        rb.linearVelocity = velocity;
    }

    Vector3 GetAimPoint()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            return hit.point;
        }

        return ray.origin + ray.direction * maxDistance;
    }

    Vector3 AdjustPoint(Vector3 origin, Vector3 target, float radius)
    {
        Vector3 dir = (target - origin).normalized;
        return target - dir * radius;
    }

    Vector3 CalculateVelocity(Vector3 origin, Vector3 target, float height)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);

        Vector3 displacement = target - origin;
        Vector3 displacementXZ = new Vector3(displacement.x, 0, displacement.z);

        if (displacementXZ.magnitude < 0.1f)
        {
            return Vector3.up * 5f;
        }

        float heightOffset = target.y - origin.y;

        height = Mathf.Max(height, heightOffset + 0.5f);

        float timeUp = Mathf.Sqrt(2 * height / gravity);
        float timeDown = Mathf.Sqrt(2 * (height - heightOffset) / gravity);

        float totalTime = timeUp + timeDown;

        if (totalTime <= 0 || float.IsNaN(totalTime))
        {
            return Vector3.up * 5f;
        }

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / totalTime;

        Vector3 result = velocityXZ + velocityY;

        if (float.IsNaN(result.x) || float.IsNaN(result.y) || float.IsNaN(result.z))
        {
            return Vector3.up * 5f;
        }

        return result;
    }
}