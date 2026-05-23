using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [Header("HUD")]
    public string weaponName;
    public Sprite weaponIcon;

    public WeaponBase weapon;

    void Awake()
    {
        weapon = GetComponent<WeaponBase>();

        if (weapon == null)
        {
            Debug.LogWarning(
                gameObject.name +
                " no tiene WeaponBase."
            );
        }
    }
}