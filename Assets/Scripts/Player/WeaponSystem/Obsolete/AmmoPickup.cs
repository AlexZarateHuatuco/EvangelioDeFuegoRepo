using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Tipo y cantidad de munición")]
    //[SerializeField] private BulletType bulletType;
    //[SerializeField] private int ammoAmount = 30;

    [Header("Feedback")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        WeaponManager manager = other.GetComponent<WeaponManager>();
        if (manager == null)
            manager = other.GetComponentInChildren<WeaponManager>();
        if (manager == null)
            manager = other.GetComponentInParent<WeaponManager>();

        if (manager == null) return;

        //bool added = manager.AddAmmoToWeapon(bulletType, ammoAmount);
        //if (!added)
        //{
        //    Debug.Log($"[AmmoPickup] Ningún arma usa el tipo {bulletType}.");
        //    return;
        //}

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}