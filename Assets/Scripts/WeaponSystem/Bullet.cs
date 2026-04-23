using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public float bulletSpeed = 3f;
    public GameObject impactEffect;
    public GameObject ParentObject;
    public Camera cam;
    Rigidbody rbBala;

    void Start()
    {
        Destroy(ParentObject, lifeTime);
        rbBala = GetComponent<Rigidbody>();
        AsignarCamara();

        //se usa para mover la bala
        if (rbBala != null)
        {
            rbBala.linearVelocity = cam.transform.forward * bulletSpeed;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, collision.contacts[0].point,Quaternion.LookRotation(collision.contacts[0].normal));
        }

        Destroy(ParentObject);
    }
    void AsignarCamara()
    {
        GameObject camObj = GameObject.Find("Camera");
        if (camObj != null)
        {
            cam = camObj.GetComponent<Camera>();
        }
    }
}