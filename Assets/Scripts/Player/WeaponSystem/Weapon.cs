/*
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;

    [Header("Fire Point")]
    [SerializeField] private Transform firePoint;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private int currentAmmoInClip;
    private int currentAmmoReserve;
    private bool isReloading = false;
    private float nextFireTime = 0f;
    private bool isBursting = false;
    private float nextEmptySoundTime = 0f;
    private const float EMPTY_SOUND_COOLDOWN = 0.4f;

    public WeaponData Data => data;
    public int CurrentAmmo => currentAmmoInClip;
    public int MaxAmmo => data != null ? data.maxAmmoInClip : 0;
    public int ReserveAmmo => currentAmmoReserve;
    public bool IsReloading => isReloading;

    public event System.Action OnAmmoChanged;
    public event System.Action OnReloadStateChanged;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;

        if (data != null)
            Initialize(data);
    }

    public void Initialize(WeaponData weaponData)
    {
        data = weaponData;
        currentAmmoInClip = data.maxAmmoInClip;
        currentAmmoReserve = data.maxAmmoReserve;
    }

    public void TryShoot(Vector3 targetPoint)
    {
        if (data == null || isReloading) return;
        if (Time.time < nextFireTime) return;
        if (isBursting) return;

        if (currentAmmoInClip <= 0)
        {
            if (Time.time >= nextEmptySoundTime)
            {
                PlaySound(data.emptySound);
                nextEmptySoundTime = Time.time + EMPTY_SOUND_COOLDOWN;
            }

            if (currentAmmoReserve > 0)
                StartCoroutine(ReloadCoroutine());
            return;
        }

        Shoot(targetPoint);
    }

    private void Shoot(Vector3 targetPoint)
    {
        if (data.bulletPrefab == null)
        {
            Debug.LogWarning($"[{data.weaponName}] No hay prefab de bala asignado.");
            return;
        }

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        Vector3 baseDirection = (targetPoint - spawnPos).normalized;

        switch (data.fireMode)
        {
            case FireMode.Single:
                ConsumAmmoAndCooldown();
                SpawnBullet(spawnPos, baseDirection);
                SpawnMuzzleFlash(spawnPos, baseDirection);
                PlaySound(data.shootSound);
                break;

            case FireMode.Shotgun:
                ConsumAmmoAndCooldown();
                for (int i = 0; i < data.pelletsCount; i++)
                {
                    Vector3 spreadDir = ApplySpread(baseDirection, data.spreadAngle);
                    SpawnBullet(spawnPos, spreadDir);
                }
                SpawnMuzzleFlash(spawnPos, baseDirection);
                PlaySound(data.shootSound);
                break;

            case FireMode.Burst:
                ConsumAmmoAndCooldown();
                StartCoroutine(BurstCoroutine(spawnPos, baseDirection));
                break;
        }
    }

    private void ConsumAmmoAndCooldown()
    {
        nextFireTime = Time.time + data.fireRate;
        currentAmmoInClip--;
        OnAmmoChanged?.Invoke();
    }

    private void SpawnBullet(Vector3 spawnPos, Vector3 direction)
    {
        GameObject bulletGO = Instantiate(data.bulletPrefab, spawnPos, Quaternion.LookRotation(direction));

        if (data.bulletType == BulletType.Grenade)
        {
            Grenade grenade = bulletGO.GetComponent<Grenade>();
            if (grenade != null)
            {
                grenade.damage = data.damage;
                Rigidbody rgb = bulletGO.GetComponent<Rigidbody>();
                if (rgb != null)
                {
                    rgb.useGravity = true;
                    rgb.linearVelocity = direction * data.bulletSpeed;
                }
            }
        }
        else
        {
            BulletPlayer bullet = bulletGO.GetComponent<BulletPlayer>();
            if (bullet != null)
            {
                bullet._bulletType = data.bulletType;
                bullet.Damage = data.damage;
                bullet.Range = data.range;
                bullet.Launch(direction, data.bulletSpeed);
            }
        }
    }

    private IEnumerator BurstCoroutine(Vector3 spawnPos, Vector3 baseDirection)
    {
        isBursting = true;

        for (int i = 0; i < data.burstCount; i++)
        {
            if (currentAmmoInClip < 0)
            {
                currentAmmoInClip = 0;
                OnAmmoChanged?.Invoke();
                break;
            }

            SpawnBullet(spawnPos, baseDirection);
            SpawnMuzzleFlash(spawnPos, baseDirection);

            if (i > 0)
            {
                currentAmmoInClip--;
                OnAmmoChanged?.Invoke();
            }

            yield return new WaitForSeconds(data.burstFireRate);
        }

        isBursting = false;
    }

    private Vector3 ApplySpread(Vector3 direction, float angle)
    {
        float half = angle / 2f;
        Quaternion spread = Quaternion.Euler(
            Random.Range(-half, half),
            Random.Range(-half, half),
            0f
        );
        return spread * direction;
    }

    private void SpawnMuzzleFlash(Vector3 spawnPos, Vector3 direction)
    {
        if (data.muzzleFlashPrefab == null) return;
        GameObject flash = Instantiate(data.muzzleFlashPrefab, spawnPos, Quaternion.LookRotation(direction));
        Destroy(flash, 0.1f);
    }

    public void TryReload()
    {
        if (isReloading) return;
        if (currentAmmoInClip >= data.maxAmmoInClip) return;
        if (currentAmmoReserve <= 0) return;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        OnReloadStateChanged?.Invoke();

        yield return new WaitForSeconds(data.reloadTime);

        int needed = data.maxAmmoInClip - currentAmmoInClip;
        int taken = Mathf.Min(needed, currentAmmoReserve);
        currentAmmoInClip += taken;
        currentAmmoReserve -= taken;
        isReloading = false;

        OnAmmoChanged?.Invoke();
        OnReloadStateChanged?.Invoke();
    }

    public void AddAmmo(int amount)
    {
        currentAmmoReserve = Mathf.Min(currentAmmoReserve + amount, data.maxAmmoReserve * 2);
        OnAmmoChanged?.Invoke();
        Debug.Log($"[{data.weaponName}] +{amount} munición. Reserva: {currentAmmoReserve}");
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        audioSource.PlayOneShot(clip);
    }
}
*/
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;

    [Header("Fire Point")]
    [SerializeField] private Transform firePoint;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private int currentAmmoInClip;
    private int currentAmmoReserve;

    private bool isReloading = false;
    private bool isBursting = false;

    private float nextFireTime = 0f;
    private float nextEmptySoundTime = 0f;

    private const float EMPTY_SOUND_COOLDOWN = 0.4f;

    public WeaponData Data => data;
    public int CurrentAmmo => currentAmmoInClip;
    public int MaxAmmo => data != null ? data.maxAmmoInClip : 0;
    public int ReserveAmmo => currentAmmoReserve;
    public bool IsReloading => isReloading;

    public event System.Action OnAmmoChanged;
    public event System.Action OnReloadStateChanged;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;

        if (data != null)
            Initialize(data);
    }

    public void Initialize(WeaponData weaponData)
    {
        data = weaponData;

        currentAmmoInClip = data.maxAmmoInClip;
        currentAmmoReserve = data.maxAmmoReserve;
    }

    public void TryShoot(Vector3 targetPoint)
    {
        if (data == null || isReloading)
            return;

        if (Time.time < nextFireTime)
            return;

        if (isBursting)
            return;

        if (currentAmmoInClip <= 0)
        {
            if (Time.time >= nextEmptySoundTime)
            {
                PlaySound(data.emptySound);
                nextEmptySoundTime = Time.time + EMPTY_SOUND_COOLDOWN;
            }

            if (currentAmmoReserve > 0)
                StartCoroutine(ReloadCoroutine());

            return;
        }

        Shoot(targetPoint);
    }

    private void Shoot(Vector3 targetPoint)
    {
        if (data.bulletPrefab == null)
        {
            Debug.LogWarning($"[{data.weaponName}] No hay prefab de bala asignado.");
            return;
        }

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        Vector3 baseDirection = (targetPoint - spawnPos).normalized;

        switch (data.fireMode)
        {
            case FireMode.Single:
                ConsumeAmmoAndCooldown();

                SpawnBullet(spawnPos, baseDirection);

                SpawnMuzzleFlash(spawnPos, baseDirection);

                PlaySound(data.shootSound);
                break;

            case FireMode.Shotgun:
                ConsumeAmmoAndCooldown();

                for (int i = 0; i < data.pelletsCount; i++)
                {
                    Vector3 spreadDir = ApplySpread(baseDirection, data.spreadAngle);

                    SpawnBullet(spawnPos, spreadDir);
                }

                SpawnMuzzleFlash(spawnPos, baseDirection);

                PlaySound(data.shootSound);
                break;

            case FireMode.Burst:
                ConsumeAmmoAndCooldown();

                StartCoroutine(BurstCoroutine(spawnPos, baseDirection));
                break;
        }
    }

    private void ConsumeAmmoAndCooldown()
    {
        nextFireTime = Time.time + data.fireRate;

        currentAmmoInClip--;

        OnAmmoChanged?.Invoke();
    }

    private void SpawnBullet(Vector3 spawnPos, Vector3 direction)
    {
        GameObject bulletGO = Instantiate(
            data.bulletPrefab,
            spawnPos,
            Quaternion.LookRotation(direction));

        /*if (data.bulletType == BulletType.Grenade)
        {
            Grenade grenade = bulletGO.GetComponent<Grenade>();

            if (grenade != null)
            {
                grenade.damage = data.damage;

                Rigidbody rb = bulletGO.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.useGravity = true;

                    // Mantiene la velocidad desde WeaponData.
                    rb.linearVelocity = direction * data.bulletSpeed;
                }
            }
        } 
        else
        {*/
            BulletPlayer bullet = bulletGO.GetComponent<BulletPlayer>();

            if (bullet != null)
            {
                bullet._bulletType = data.bulletType;
                bullet.Damage = data.damage;
                bullet.Range = data.range;

                bullet.Launch(direction);
            }
        //}
    }

    private IEnumerator BurstCoroutine(Vector3 spawnPos, Vector3 baseDirection)
    {
        isBursting = true;

        for (int i = 0; i < data.burstCount; i++)
        {
            if (currentAmmoInClip < 0)
            {
                currentAmmoInClip = 0;

                OnAmmoChanged?.Invoke();

                break;
            }

            SpawnBullet(spawnPos, baseDirection);

            SpawnMuzzleFlash(spawnPos, baseDirection);

            if (i > 0)
            {
                currentAmmoInClip--;

                OnAmmoChanged?.Invoke();
            }

            yield return new WaitForSeconds(data.burstFireRate);
        }

        isBursting = false;
    }

    private Vector3 ApplySpread(Vector3 direction, float angle)
    {
        float half = angle / 2f;

        Quaternion spread = Quaternion.Euler(
            Random.Range(-half, half),
            Random.Range(-half, half),
            0f);

        return spread * direction;
    }

    private void SpawnMuzzleFlash(Vector3 spawnPos, Vector3 direction)
    {
        if (data.muzzleFlashPrefab == null)
            return;

        GameObject flash = Instantiate(
            data.muzzleFlashPrefab,
            spawnPos,
            Quaternion.LookRotation(direction));

        Destroy(flash, 0.1f);
    }

    public void TryReload()
    {
        if (isReloading)
            return;

        if (currentAmmoInClip >= data.maxAmmoInClip)
            return;

        if (currentAmmoReserve <= 0)
            return;

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        OnReloadStateChanged?.Invoke();

        yield return new WaitForSeconds(data.reloadTime);

        int needed = data.maxAmmoInClip - currentAmmoInClip;

        int taken = Mathf.Min(needed, currentAmmoReserve);

        currentAmmoInClip += taken;
        currentAmmoReserve -= taken;

        isReloading = false;

        OnAmmoChanged?.Invoke();
        OnReloadStateChanged?.Invoke();
    }

    public void AddAmmo(int amount)
    {
        currentAmmoReserve = Mathf.Min(
            currentAmmoReserve + amount,
            data.maxAmmoReserve * 2);

        OnAmmoChanged?.Invoke();

        Debug.Log($"[{data.weaponName}] +{amount} munición. Reserva: {currentAmmoReserve}");
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null)
            return;

        audioSource.PlayOneShot(clip);
    }
}