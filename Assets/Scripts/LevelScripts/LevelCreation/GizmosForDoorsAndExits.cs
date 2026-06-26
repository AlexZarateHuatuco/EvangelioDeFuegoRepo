using UnityEngine;

public class GizmosForDoorsAndExits : MonoBehaviour
{
    //a ser usado con empty gameobjects

    [SerializeField] private Color arrowColor = Color.green;   // green man was here
    //ah red es para salidas
    [SerializeField] private float arrowLength = 1.5f;
    [SerializeField] private float arrowHeadSize = 0.3f;

    private void OnDrawGizmos()
    {

        Gizmos.color = arrowColor;
        Vector3 forward = transform.forward;
        Gizmos.DrawRay(transform.position, forward * arrowLength);


        Vector3 tip = transform.position + forward * arrowLength;
        Vector3 right = transform.right * arrowHeadSize * 0.5f;
        Vector3 up = transform.up * arrowHeadSize * 0.5f;

        Gizmos.DrawLine(tip, tip - (forward * arrowHeadSize) + right);
        Gizmos.DrawLine(tip, tip - (forward * arrowHeadSize) - right);
        Gizmos.DrawLine(tip, tip - (forward * arrowHeadSize) + up);
        Gizmos.DrawLine(tip, tip - (forward * arrowHeadSize) - up);


        //chat.... lamento informar q... use ia para esto ;-;
        // no supe que otra forma!! tal vez solo el cono? pero solo se usar esferas y cubos.
    }
}
