/*using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Prefabs")]
    public GameObject riflePrefab;
    public GameObject shotgunPrefab;
    public GameObject GrenadeHandPrefab;

    [Header("Holder")]
    public Transform weaponHolder;

    private GameObject rifleInstance;
    private GameObject shotgunInstance;

    private GameObject currentWeapon;

    private int currentWeaponIndex = 0;

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("WeaponSwitch: No hay cámara asignada.");
            return;
        }

        rifleInstance = Instantiate(riflePrefab, weaponHolder);
        shotgunInstance = Instantiate(shotgunPrefab, weaponHolder);

        rifleInstance.transform.localPosition = Vector3.zero;
        rifleInstance.transform.localRotation = Quaternion.identity;

        shotgunInstance.transform.localPosition = Vector3.zero;
        shotgunInstance.transform.localRotation = Quaternion.identity;

        rifleInstance.SetActive(true);
        shotgunInstance.SetActive(false);
        grenadeHandInstance.SetActive(false);

        currentWeapon = rifleInstance;

        AssignCamera(rifleInstance);
        AssignCamera(shotgunInstance);
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
        currentWeapon.SetActive(false);

        currentWeaponIndex++;

        if (currentWeaponIndex > 2)
        {
            currentWeaponIndex = 0;
        }

        switch (currentWeaponIndex)
        {
            case 0:
                currentWeapon = rifleInstance;
                break;

            case 1:
                currentWeapon = shotgunInstance;
                break;

            default:
                break;
        }

        currentWeapon.SetActive(true);
    }

    void AssignCamera(GameObject weapon)
    {
        var rifle = weapon.GetComponent<RifleShot>();
        if (rifle != null)
        {
            rifle.cam = playerCamera;
            return;
        }

        var shotgun = weapon.GetComponent<ShotgunShot>();
        if (shotgun != null)
        {
            shotgun.cam = playerCamera;
            return;
        }
        Debug.LogWarning("Arma sin script reconocido.");
    }
}*/
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