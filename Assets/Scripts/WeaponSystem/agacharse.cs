using UnityEngine;

public class agacharse : MonoBehaviour
{
    public CapsuleCollider capsule;

    public float alturaNormal = 2f;
    public float alturaAgachado = 1f;

    public Vector3 centroNormal = new Vector3(0, 1f, 0);
    public Vector3 centroAgachado = new Vector3(0, 0.5f, 0);

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            capsule.height = alturaAgachado;
            capsule.center = centroAgachado;
        }
        else
        {
            capsule.height = alturaNormal;
            capsule.center = centroNormal;
        }
    }
}
