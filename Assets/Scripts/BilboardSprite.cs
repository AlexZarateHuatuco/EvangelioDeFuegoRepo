using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;

        Vector3 lookPos = transform.position - mainCamera.transform.position;
        lookPos.y = 0f;

        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}