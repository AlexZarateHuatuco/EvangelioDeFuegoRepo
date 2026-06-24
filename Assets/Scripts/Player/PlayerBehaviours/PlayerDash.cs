using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashForce;
    [SerializeField] float dashIFrames;
    public Rigidbody rb;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }
    void Dash()
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
    }
}
