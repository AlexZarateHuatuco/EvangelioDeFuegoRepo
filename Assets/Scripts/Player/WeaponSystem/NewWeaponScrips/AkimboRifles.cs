using UnityEngine;

public class AkimboRifles : MonoBehaviour 
{ 



[Header("Gun Muzzles")]
//crear empty en los barriles!
[SerializeField] private Transform leftMuzzle;    
[SerializeField] private Transform rightMuzzle;   

[Header("Aiming")]
[SerializeField] private Camera playerCamera;
[SerializeField] private float maxRange = 100f;
[SerializeField] private LayerMask enemyLayer;    // layer(s) your enemies are on
[SerializeField] private LayerMask blockingLayers = ~0;  // defaults to everything (blocks ray)

[Header("Damage")]
[SerializeField] private float damage = 1f;       // normal shot damage

[Header("Particles")]
[SerializeField] private GameObject muzzleFlashPrefab;
[SerializeField] private GameObject impactEffectPrefab;

public enum GunSide
{
        Left,
        Right
}

private void Awake()
{
    if (playerCamera == null)
        playerCamera = Camera.main;
}

private void Update()
{

    if (Input.GetButtonDown("Fire1"))      
        Shoot(leftMuzzle, GunSide.Left);

    if (Input.GetButtonDown("Fire2"))      
        Shoot(rightMuzzle, GunSide.Right);
}

private void Shoot(Transform muzzle, GunSide gunSide)
{
    // 1. Muzzle flash from the gun barrel
    if (muzzleFlashPrefab != null && muzzle != null)
        Instantiate(muzzleFlashPrefab, muzzle.position, muzzle.rotation);

    // 2. Raycast from screen centre for aiming
    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
    bool didHit = Physics.Raycast(ray, out RaycastHit hit, maxRange, blockingLayers);

    if (didHit)
    {
        // 3. Impact particle at hit point (aligned with surface normal)
        if (impactEffectPrefab != null)
            Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

        if ((enemyLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
        {
            // Try the vulnerability component first (your akimbo system)
            EnemyVulnerability ev = hit.collider.GetComponent<EnemyVulnerability>();
            if (ev != null)
            {
                ev.HitByGun(gunSide, damage);
            }
            else
            {

                    EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                    if (health != null)
                    health.TakeDamage(damage);
            }
        }
    }
}
}