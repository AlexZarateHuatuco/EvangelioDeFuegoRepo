using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Prefabs")]
    public GameObject riflePrefab;
    public GameObject shotgunPrefab;

    [Header("Holder")]
    public Transform weaponHolder;

    private GameObject rifleInstance;
    private GameObject shotgunInstance;

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

        rifleInstance.transform.localPosition = Vector3.zero;
        rifleInstance.transform.localRotation = Quaternion.identity;

        shotgunInstance.transform.localPosition = Vector3.zero;
        shotgunInstance.transform.localRotation = Quaternion.identity;

        rifleInstance.SetActive(true);
        shotgunInstance.SetActive(false);

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
}