/*
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("Armas del jugador")]
    public GameObject[] weapons;

    private int currentWeaponIndex = 0;

    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        weapons[currentWeaponIndex].SetActive(false);

        currentWeaponIndex++;

        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        weapons[currentWeaponIndex].SetActive(true);
    }
}
*/
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [Header("Cámara del jugador")]
    public Camera playerCamera;

    [Header("Armas del jugador")]
    public GameObject[] weapons;

    private int currentWeaponIndex = 0;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;

            if (playerCamera == null)
                Debug.LogError("WeaponSwitch: No se encontró una cámara principal.");
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }

        AssignCamera(weapons[currentWeaponIndex]);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            SwitchWeapon();
    }

    void SwitchWeapon()
    {
        weapons[currentWeaponIndex].SetActive(false);

        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;

        weapons[currentWeaponIndex].SetActive(true);
        AssignCamera(weapons[currentWeaponIndex]);
    }

    void AssignCamera(GameObject weapon)
    {
        var rifle = weapon.GetComponentInChildren<RifleShot>();
        if (rifle != null)
        {
            rifle.cam = playerCamera;
            return;
        }

        var shotgun = weapon.GetComponentInChildren<ShotgunShot>();
        if (shotgun != null)
        {
            shotgun.cam = playerCamera;
            return;
        }

        Debug.LogWarning("WeaponSwitch: " + weapon.name + " no tiene script de arma reconocido.");
    }
}