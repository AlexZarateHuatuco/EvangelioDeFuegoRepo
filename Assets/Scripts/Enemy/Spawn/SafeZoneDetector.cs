using UnityEngine;

public class SafeZoneDetector : MonoBehaviour
{
    public bool IsInSafeZone { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            IsInSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            IsInSafeZone = false;
        }
    }
}