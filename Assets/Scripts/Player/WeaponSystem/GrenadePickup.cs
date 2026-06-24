using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    [Header("Cantidad")]
    [SerializeField] private int amount = 1;

    [Header("Feedback")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GrenadeLauncher launcher = other.GetComponent<GrenadeLauncher>();
        if (launcher == null)
            launcher = other.GetComponentInChildren<GrenadeLauncher>();
        if (launcher == null)
            launcher = other.GetComponentInParent<GrenadeLauncher>();

        if (launcher == null) return;

        bool added = launcher.AddGrenades(amount);
        if (!added)
        {
            Debug.Log("[GrenadePickup] El jugador ya tiene granadas al máximo.");
            return;
        }

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}