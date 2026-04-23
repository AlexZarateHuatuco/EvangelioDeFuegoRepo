using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    //public GameObject impactEffect;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    /*void OnTriggerEnter(Collider col)
    {
        //if (impactEffect != null)
        //{
        //    Instantiate(
        //        impactEffect,
        //        col.contacts[0].point,
        //        Quaternion.identity
        //    );
        //}
        if (col.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }*/
}