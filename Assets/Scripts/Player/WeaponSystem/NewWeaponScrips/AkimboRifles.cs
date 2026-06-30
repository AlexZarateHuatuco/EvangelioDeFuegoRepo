using UnityEngine;
using System.Collections;

public class AkimboRifles : MonoBehaviour
{
    public enum GunSide
    {
        Left,
        Right
    }

    [Header("Gun Muzzles")]
    //crear empty en los barriles! el empty es de donde salen los efectos
    [SerializeField] private Transform leftMuzzle;
    [SerializeField] private Transform rightMuzzle;

    [Header("Aiming")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxRange = 100f;
    [SerializeField] private LayerMask blockingLayers = ~0;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 15;
    private int leftAmmo;
    private int rightAmmo;

    [Header("Reload")]
    [SerializeField] private float reloadTime = 5f;
    private bool isReloading;

    [Header("Particles")]
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject impactEffectPrefab;

    public static AkimboRifles Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        if (playerCamera == null)
            playerCamera = Camera.main;

        leftAmmo = maxAmmo;
        rightAmmo = maxAmmo;
    }

    private void Update()
    {
        // Can't shoot while reloading
        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1") && leftAmmo > 0)
            Shoot(leftMuzzle, GunSide.Left, ref leftAmmo);

        if (Input.GetButtonDown("Fire2") && rightAmmo > 0)
            Shoot(rightMuzzle, GunSide.Right, ref rightAmmo);

        // R reloads both guns at once
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading && (leftAmmo < maxAmmo || rightAmmo < maxAmmo))
                StartCoroutine(ReloadBoth());
        }
    }

    private void Shoot(Transform muzzle, GunSide gunSide, ref int ammo)
    {
        ammo--;

        if (muzzleFlashPrefab != null && muzzle != null)
            Instantiate(muzzleFlashPrefab, muzzle.position, muzzle.rotation);

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        bool didHit = Physics.Raycast(ray, out RaycastHit hit, maxRange, blockingLayers);

        if (didHit)
        {
            if (impactEffectPrefab != null)
                Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            EnemyVulnerability ev = hit.collider.GetComponent<EnemyVulnerability>();
            if (ev != null)
            {
                ev.HitByGun(gunSide);
            }
            else
            {
                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                if (health != null)
                    health.TakeDamage(1f);
            }
        }
    }

    private IEnumerator ReloadBoth()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        leftAmmo = maxAmmo;
        rightAmmo = maxAmmo;
        isReloading = false;
    }

    public void AddAmmo(GunSide gun, int amount)
    {
        if (gun == GunSide.Left)
            leftAmmo = Mathf.Min(leftAmmo + amount, maxAmmo);
        else
            rightAmmo = Mathf.Min(rightAmmo + amount, maxAmmo);
    }

    public int GetAmmo(GunSide side)
    {
        if (side == GunSide.Left)
        {
            return leftAmmo;
        }

        else
        {
            return rightAmmo;
        }
    }

    public bool IsReloading()
    {
        return isReloading;
    }
}