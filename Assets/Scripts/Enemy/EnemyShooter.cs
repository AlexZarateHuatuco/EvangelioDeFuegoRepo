using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
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

        float distance = Vector3.Distance(transform.position,player.position);

        if (distance > detectionRange)
        {
            return;
        }

        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

        RaycastHit hit;

        Debug.DrawRay(firePoint.position,directionToPlayer * detectionRange,Color.red);

        if (Physics.Raycast(firePoint.position,directionToPlayer,out hit,detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
               
                Vector3 targetPosition = new Vector3(player.position.x,firePoint.position.y,player.position.z);

                firePoint.LookAt(targetPosition);

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
        Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);

        Debug.Log("Enemigo disparó");
    }
}