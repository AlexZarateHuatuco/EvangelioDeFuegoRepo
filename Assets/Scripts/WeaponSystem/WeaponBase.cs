using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Munición")]
    public int magazineSize = 30;
    public float reloadTime = 2f;

    protected int currentAmmo;

    protected virtual void Start()
    {
        currentAmmo = magazineSize;
    }

    public int CurrentAmmo => currentAmmo;
    public int MaxAmmo => magazineSize;

    public abstract void Shoot();
}