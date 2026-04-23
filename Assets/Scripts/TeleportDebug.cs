using UnityEngine;

public class TeleportDebug : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            transform.position = new Vector3(0f, 1.76f, -1f);
        }
    }
}
