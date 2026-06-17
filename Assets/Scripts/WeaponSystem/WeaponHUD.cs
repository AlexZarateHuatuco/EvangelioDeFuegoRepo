/*
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHUD : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI grenadeText;

    [Header("Iconos")]
    public Image weaponIcon;
    public Image grenadeIcon;

    [Header("Sprite de granada")]
    public Sprite grenadeSprite;

    [Header("Jugador")]
    public GameObject player;

    [Header("Granadas")]
    public GrenadeThrow grenadeSystem;

    private WeaponInfo currentWeapon;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                Debug.LogError("No se encontró Player");
                enabled = false;
                return;
            }
        }

        if (grenadeSystem == null)
        {
            grenadeSystem =
                player.GetComponentInChildren<GrenadeThrow>(true);
        }

        if (grenadeIcon != null &&
            grenadeSprite != null)
        {
            grenadeIcon.sprite =
                grenadeSprite;
        }
    }

    void Update()
    {
        DetectCurrentWeapon();
        UpdateHUD();
    }

    void DetectCurrentWeapon()
    {
        currentWeapon = null;

        WeaponInfo[] weapons =
            player.GetComponentsInChildren<WeaponInfo>(true);

        foreach (WeaponInfo weapon in weapons)
        {
            if (weapon == null)
                continue;

            if (weapon.weapon == null)
                continue;

            if (weapon.weapon.weaponType ==
                WeaponType.Grenade)
            {
                continue;
            }

            if (weapon.gameObject.activeInHierarchy)
            {
                currentWeapon = weapon;
                return;
            }
        }
    }

    void UpdateHUD()
    {
        if (currentWeapon != null)
        {
            ammoText.text =
                currentWeapon.weapon.CurrentAmmo +
                "/" +
                currentWeapon.weapon.MaxAmmo;

            if (currentWeapon.weaponIcon != null)
            {
                weaponIcon.sprite =
                    currentWeapon.weaponIcon;
            }
        }
        else
        {
            ammoText.text = "--/--";
        }

        if (grenadeSystem != null)
        {
            grenadeText.text =
                grenadeSystem.CurrentAmmo.ToString();
        }
        else
        {
            grenadeText.text = "0";
        }
    }
}
*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHUD : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GrenadeLauncher grenadeLauncher;

    [Header("Arma activa")]
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI reserveAmmoText;
    [SerializeField] private TextMeshProUGUI reloadingText;

    [Header("Granadas")]
    [SerializeField] private TextMeshProUGUI grenadeCountText;

    [Header("Colores")]
    [SerializeField] private Color lowAmmoColor = Color.red;
    [SerializeField] private Color normalAmmoColor = Color.white;

    private Weapon trackedWeapon;

    private void Start()
    {
        if (weaponManager == null)
            weaponManager = FindFirstObjectByType<WeaponManager>();

        if (grenadeLauncher == null)
            grenadeLauncher = FindFirstObjectByType<GrenadeLauncher>();

        if (weaponManager != null)
            weaponManager.OnWeaponChanged += OnWeaponChanged;

        if (grenadeLauncher != null)
        {
            grenadeLauncher.OnGrenadesChanged += UpdateGrenadeDisplay;
            UpdateGrenadeDisplay();
        }
    }

    private void OnDestroy()
    {
        if (weaponManager != null)
            weaponManager.OnWeaponChanged -= OnWeaponChanged;

        if (grenadeLauncher != null)
            grenadeLauncher.OnGrenadesChanged -= UpdateGrenadeDisplay;

        Unsubscribe(trackedWeapon);
    }

    private void OnWeaponChanged(Weapon newWeapon, int index)
    {
        Unsubscribe(trackedWeapon);

        trackedWeapon = newWeapon;

        if (weaponNameText != null)
            weaponNameText.text = newWeapon.Data.weaponName;

        if (weaponIcon != null)
            weaponIcon.sprite = newWeapon.Data.weaponIcon;

        trackedWeapon.OnAmmoChanged += UpdateAmmoDisplay;
        trackedWeapon.OnReloadStateChanged += UpdateReloadDisplay;

        UpdateAmmoDisplay();
        UpdateReloadDisplay();
    }

    private void Unsubscribe(Weapon weapon)
    {
        if (weapon == null) return;
        weapon.OnAmmoChanged -= UpdateAmmoDisplay;
        weapon.OnReloadStateChanged -= UpdateReloadDisplay;
    }

    private void UpdateAmmoDisplay()
    {
        if (trackedWeapon == null) return;

        int current = trackedWeapon.CurrentAmmo;
        int max = trackedWeapon.MaxAmmo;
        int reserve = trackedWeapon.ReserveAmmo;

        if (ammoText != null)
        {
            ammoText.text = $"{current} / {max}";
            ammoText.color = (current <= Mathf.CeilToInt(max * 0.25f)) ? lowAmmoColor : normalAmmoColor;
        }

        if (reserveAmmoText != null)
            reserveAmmoText.text = reserve.ToString();
    }

    private void UpdateReloadDisplay()
    {
        if (trackedWeapon == null) return;

        if (reloadingText != null)
            reloadingText.gameObject.SetActive(trackedWeapon.IsReloading);
    }

    private void UpdateGrenadeDisplay()
    {
        if (grenadeCountText != null && grenadeLauncher != null)
            grenadeCountText.text = grenadeLauncher.CurrentGrenades.ToString();
    }
}