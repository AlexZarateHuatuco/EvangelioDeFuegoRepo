/*
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movimiento")]
    public float moveSpeed = 5f;

    /*[Header("Salto")]
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public int maxJumps = 2;*

    [Header("Mouse")]
    public float mouseSensitivity = 200f;
    public Transform cameraPivot;

    private float xRotation;
    private float verticalVelocity;
    private int jumpCount;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ---------- MOUSE ----------
        float mouseX = Input.GetAxis("Mouse X") *
                       mouseSensitivity *
                       Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") *
                       mouseSensitivity *
                       Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraPivot.localRotation =
            Quaternion.Euler(xRotation, 0, 0);

        // ---------- MOVIMIENTO ----------
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move =
            (transform.right * x +
             transform.forward * z).normalized;

        // ---------- SUELO ----------
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            jumpCount = 0;
        }

        // ---------- SALTO ----------
        /*if (Input.GetKeyDown(KeyCode.Space)
            && jumpCount < maxJumps)
        {
            verticalVelocity =
                Mathf.Sqrt(
                    jumpHeight *
                    -2f *
                    gravity
                );

            jumpCount++;
        }*

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = move * moveSpeed;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);

    }
}
*/
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float gravity = -20f;

    /*[Header("Salto")]
    public float jumpHeight = 2f;
    public int maxJumps = 2;*/

    [Header("Mouse")]
    public float horizontalSensitivity = 200f;
    public float verticalSensitivity = 200f;
    public Transform cameraPivot;

    private float xRotation;
    private float verticalVelocity;
    private int jumpCount;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        horizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity", horizontalSensitivity);
        verticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity", verticalSensitivity);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ---------- MOUSE ----------
        float mouseX = Input.GetAxisRaw("Mouse X") *
                       horizontalSensitivity *
                       Time.deltaTime;

        float mouseY = Input.GetAxisRaw("Mouse Y") *
                       verticalSensitivity *
                       Time.deltaTime;

        // Rotación horizontal del jugador
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical de la cámara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraPivot.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        // ---------- MOVIMIENTO ----------
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move =
            (transform.right * x +
             transform.forward * z).normalized;

        // ---------- SUELO ----------
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            jumpCount = 0;
        }

        // ---------- SALTO ----------
        /*if (Input.GetKeyDown(KeyCode.Space)
            && jumpCount < maxJumps)
        {
            verticalVelocity =
                Mathf.Sqrt(
                    jumpHeight *
                    -2f *
                    gravity
                );

            jumpCount++;
        }*/

        // ---------- GRAVEDAD ----------
        verticalVelocity += gravity * Time.deltaTime;

        // Movimiento final
        Vector3 finalMove = move * moveSpeed;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    // ---------- OPCIONES ----------
    public void SetHorizontalSensitivity(float value)
    {
        horizontalSensitivity = value;

        PlayerPrefs.SetFloat("HorizontalSensitivity", value);
        PlayerPrefs.Save();
    }

    public void SetVerticalSensitivity(float value)
    {
        verticalSensitivity = value;

        PlayerPrefs.SetFloat("VerticalSensitivity", value);
        PlayerPrefs.Save();
    }
}