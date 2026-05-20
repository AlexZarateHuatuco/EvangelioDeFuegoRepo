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
    private GameObject grenadeHandInstance;
    private GameObject currentWeapon;
    private bool isRifle = true;

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("WeaponSwitch: No hay cámara asignada.");
            return;
        }
        rifleInstance = Instantiate(riflePrefab, weaponHolder);
        shotgunInstance = Instantiate(shotgunPrefab, weaponHolder);
        grenadeHandInstance = Instantiate(grenadeHandPrefab, weaponHolder);

        rifleInstance.transform.localPosition = Vector3.zero;
        rifleInstance.transform.localRotation = Quaternion.identity;

        shotgunInstance.transform.localPosition = Vector3.zero;
        shotgunInstance.transform.localRotation = Quaternion.identity;

        grenadeHandInstance.transform.localPosition = Vector3.zero;
        grenadeHandInstance.transform.localRotation = Quaternion.identity;

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

        isRifle = !isRifle;

        if (isRifle)
            currentWeapon = rifleInstance;
        else
            currentWeapon = shotgunInstance;

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
    public Camera playerCamera;

    [Header("Prefabs")]
    public GameObject riflePrefab;
    public GameObject shotgunPrefab;
    public GameObject GrenadeHandPrefab;

    [Header("Holder")]
    public Transform weaponHolder;

    private GameObject rifleInstance;
    private GameObject shotgunInstance;
    private GameObject grenadeHandInstance;

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
        grenadeHandInstance = Instantiate(GrenadeHandPrefab, weaponHolder);

        rifleInstance.transform.localPosition = Vector3.zero;
        rifleInstance.transform.localRotation = Quaternion.identity;

        shotgunInstance.transform.localPosition = Vector3.zero;
        shotgunInstance.transform.localRotation = Quaternion.identity;

        grenadeHandInstance.transform.localPosition = Vector3.zero;
        grenadeHandInstance.transform.localRotation = Quaternion.identity;

        rifleInstance.SetActive(true);
        shotgunInstance.SetActive(false);
        grenadeHandInstance.SetActive(false);

        currentWeapon = rifleInstance;

        AssignCamera(rifleInstance);
        AssignCamera(shotgunInstance);
        AssignCamera(grenadeHandInstance);
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

            case 2:
                currentWeapon = grenadeHandInstance;
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

        var grenade = weapon.GetComponent<GrenadeThrow>();
        if (grenade != null)
        {
            grenade.cam = playerCamera;
            return;
        }

        Debug.LogWarning("Arma sin script reconocido.");
    }
}