using UnityEngine;
using System.Collections;

public class RifleShot : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;                 // Cámara REAL (no la virtual de Cinemachine)
    public Transform firePoint;
    public GameObject bulletPrefab;

    [Header("Configuración")]
    public float fireRate = 10f;
    public int magazineSize = 30;
    public float reloadTime = 2f;
    public float bulletSpeed = 20f;

    private int currentAmmo;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    void Awake()
    {

        AsignarCamara();
    }

    void Start()
    {
        currentAmmo = magazineSize;
        AsignarCamara();
    }

    void Update()
    {
        if (isReloading) return;

        if (cam == null)
        {
            Debug.LogError("No hay cámara asignada al arma");
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
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
        currentAmmo--;

        // Ray desde el centro de la pantalla
        /*Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }*/

        // Dirección desde el arma hacia el punto apuntado
        //Vector3 direction = (targetPoint - firePoint.position).normalized;

        //se usa para invocar y dirigir la bala
        Quaternion rot = Quaternion.LookRotation(cam.transform.forward);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);

        //Rigidbody rb = bullet.GetComponent<Rigidbody>();

        /*if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed; // CORRECTO
        }*/

        //Debug.DrawRay(firePoint.position, direction * 10f, Color.red, 1f);
    }

    void AsignarCamara()
    {
        GameObject camObj = GameObject.Find("Camera");
        if (camObj != null)
        {
            cam = camObj.GetComponent<Camera>();
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