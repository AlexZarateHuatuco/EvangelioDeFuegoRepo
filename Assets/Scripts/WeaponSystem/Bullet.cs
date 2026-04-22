using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public GameObject impactEffect;
    public GameObject ParentObject;

    void Start()
    {
        Destroy(ParentObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, collision.contacts[0].point,Quaternion.LookRotation(collision.contacts[0].normal));
        }
        Destroy(ParentObject);
    }
}