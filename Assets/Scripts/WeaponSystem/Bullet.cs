/*using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    //public GameObject impactEffect;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider col)
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
    }
}*/
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 40f;

    [Header("Damage")]
    public float damage = 10f;

    [Header("Weapon Reference")]
    public WeaponBase weapon;

    [Header("Lifetime")]
    public float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}