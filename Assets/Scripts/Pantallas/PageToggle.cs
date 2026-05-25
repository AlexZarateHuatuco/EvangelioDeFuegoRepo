using UnityEngine;

public class PageToggle : MonoBehaviour
{
    [SerializeField] private GameObject objectToDeactivate;
    [SerializeField] private GameObject objectToActivate;

    public void Switch()
    {
        objectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }
}
