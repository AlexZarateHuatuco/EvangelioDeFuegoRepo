using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Combat")]
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float detectionRange = 15f;

    private float nextFireTime;

    private Transform player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        // =========================
        // DISTANCE CHECK
        // =========================

        float distance = Vector3.Distance(
            transform.position,
            player.position

        );

        if (distance > detectionRange)
        {
            return;
        }

        // =========================
        // RAYCAST CHECK
        // =========================

        Vector3 directionToPlayer =
            (player.position - firePoint.position).normalized;

        RaycastHit hit;

        Debug.DrawRay(
            firePoint.position,
            directionToPlayer * detectionRange,
            Color.red
        );

        if (Physics.Raycast(
            firePoint.position,
            directionToPlayer,
            out hit,
            detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Rotación horizontal
                Vector3 targetPosition = new Vector3(
                    player.position.x,
                    firePoint.position.y,
                    player.position.z
                );

                firePoint.LookAt(targetPosition);

                // Disparo
                if (Time.time >= nextFireTime)
                {
                    Shoot();

                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    private void Shoot()
    {
        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Debug.Log("Enemigo disparó");
    }
}