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