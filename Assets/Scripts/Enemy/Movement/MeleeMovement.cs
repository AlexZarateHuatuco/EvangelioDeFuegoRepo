using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MeleeMovement : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 3.5f;

    [Header("Combate")]
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Detección de piso")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRayLength = 0.2f;

    private Rigidbody rb;
    private float attackTimer;
    private bool isGrounded;

    private enum State { Idle, Chase, Attack }
    private State currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;
        EvaluateState();
        HandleAttack();
    }

    private void FixedUpdate()
    {
        if (player == null) return;
        HandleMovement();
    }

    private void EvaluateState()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > detectionRange)
            currentState = State.Idle;
        else if (dist <= attackRange)
            currentState = State.Attack;
        else
            currentState = State.Chase;
    }

    private void HandleMovement()
    {
        CheckGrounded();

        Vector3 direction = Vector3.zero;

        if (currentState == State.Chase)
        {
            Vector3 toPlayer = player.position - transform.position;
            toPlayer.y = 0f;
            direction = toPlayer.normalized;
        }

        // Preservar la velocidad vertical del Rigidbody (gravedad)
        Vector3 velocity = direction * moveSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        if (direction.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    private void HandleAttack()
    {
        if (currentState == State.Attack && attackTimer <= 0f)
        {
            attackTimer = attackCooldown;
            SendMessage("DoAttack", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down,
            GetComponent<Collider>().bounds.extents.y + groundRayLength, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}