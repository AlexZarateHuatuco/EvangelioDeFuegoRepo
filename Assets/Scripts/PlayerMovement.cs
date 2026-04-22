using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movement;

    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Salto")]
    private int jumpCount;
    public int maxJumps = 2;
    private bool isGrounded;
    public float jumpForce = 5f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 200f;
    public Transform cameraPivot; // Cámara (hijo del jugador)
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // bloquea el cursor
    }

    void Update()
    {
        // ----------- ROTACIÓN CON MOUSE -----------
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));

        // ----------- MOVIMIENTO (WASD) -----------
        float x = Input.GetAxisRaw("Horizontal"); // A/D (strafe)
        float z = Input.GetAxisRaw("Vertical");   // W/S

        movement = (transform.right * x + transform.forward * z).normalized;

        // ----------- SALTO -----------
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        jumpCount++;
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
/*using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movement;
    public float moveSpeed = 5f;

    private int jumpCount;
    public int maxJumps = 2;
    private bool isGrounded;
    public float jumpForce = 5f;

    [Header("Rotación por pasos")]
    public float rotationStep = 90f; // grados por pulsación

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento SOLO hacia adelante/atrás
        float z = Input.GetAxisRaw("Vertical");
        movement = transform.forward * z;

        // Rotación por pulsación
        if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(-rotationStep);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(rotationStep);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }

    void Rotate(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        jumpCount++;
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}*/
/*using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movement;
    public float moveSpeed = 5f;

    private int jumpCount;
    public int maxJumps = 2;
    private bool isGrounded;
    public float jumpForce = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, 0f, z).normalized;
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }
    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount++;
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}*/