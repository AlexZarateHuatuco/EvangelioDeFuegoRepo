using UnityEngine;
using System.Collections;

public class GrenadeThrow : WeaponBase
{
    public Camera cam;
    public Transform throwPoint;
    public GameObject grenadePrefab;

    public float throwForce = 20f;
    public float grenadeCooldown = 3f;

    public float explosionRadius = 6f;
    public float grenadeDamage = 100f;
    public float explosionDelay = 1.2f;

    private bool canThrow = true;

    protected override void Start()
    {
        base.Start();
        weaponType = WeaponType.Grenade;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canThrow)
            Shoot();
    }

    public override void Shoot()
    {
        canThrow = false;

        GameObject grenade = Instantiate(
            grenadePrefab,
            throwPoint.position,
            Quaternion.identity
        );

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Vector3 dir = ray.direction + Vector3.up * 0.15f;

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = dir.normalized * throwForce;

        StartCoroutine(Explode(grenade));
        StartCoroutine(Cooldown());
    }

    IEnumerator Explode(GameObject grenade)
    {
        yield return new WaitForSeconds(explosionDelay);

        Collider[] hits = Physics.OverlapSphere(
            grenade.transform.position,
            explosionRadius
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                if (enemy != null)
                    enemy.TakeDamage(grenadeDamage);
            }
        }

        Destroy(grenade);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(grenadeCooldown);
        canThrow = true;
    }
}