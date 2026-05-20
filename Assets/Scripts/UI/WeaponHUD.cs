using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHUD : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI grenadeText;
    public Image weaponIcon;

    [Header("Granadas")]
    public int grenades = 3;

    private WeaponInfo currentWeapon;
    private Transform weaponHolder;

    void Start()
    {
        //Buscar automáticamente el WeaponHolder
        GameObject holder =
            GameObject.FindGameObjectWithTag("WeaponHolder");

        if (holder != null)
        {
            weaponHolder = holder.transform;
        }
        else
        {
            Debug.LogError(
                "No existe un objeto con el tag WeaponHolder"
            );
        }
    }

    void Update()
    {
        if (weaponHolder == null)
            return;

        DetectWeapon();
        UpdateHUD();
    }

    void DetectWeapon()
    {
        foreach (Transform child in weaponHolder)
        {
            //Detectar arma activa
            if (child.gameObject.activeInHierarchy)
            {
                WeaponInfo weapon =
                    child.GetComponent<WeaponInfo>();

                if (weapon != null)
                {
                    currentWeapon = weapon;
                    return;
                }
            }
        }
    }

    void UpdateHUD()
    {
        if (currentWeapon == null)
            return;

        //Actualizar munición
        if (currentWeapon.weapon != null)
        {
            ammoText.text =
                currentWeapon.weapon.CurrentAmmo +
                "/" +
                currentWeapon.weapon.MaxAmmo;
        }
        else
        {
            ammoText.text = "--/--";
        }

        //Actualizar granadas
        grenadeText.text = grenades.ToString();

        //Actualizar icono
        if (currentWeapon.weaponIcon != null)
        {
            weaponIcon.sprite =
                currentWeapon.weaponIcon;
        }
    }

    public void AddGrenade(int amount)
    {
        grenades += amount;
    }

    public void UseGrenade()
    {
        if (grenades > 0)
        {
            grenades--;
        }
    }
}