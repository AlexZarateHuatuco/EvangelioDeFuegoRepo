using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RangedMovement : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Combate")]
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float minRange = 4f;
    [SerializeField] private float attackCooldown = 1.5f;

    [Header("Detección de piso")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRayLength = 0.2f;

    private Rigidbody rb;
    private Collider col;
    private float attackTimer;
    private bool isGrounded;

    private enum State { Chase, InRange, TooClose }
    private State currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

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

        if (dist <= minRange)
            currentState = State.TooClose;
        else if (dist <= attackRange)
            currentState = State.InRange;
        else
            currentState = State.Chase;
    }

    private void HandleMovement()
    {
        CheckGrounded();

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;

        Vector3 direction = Vector3.zero;

        switch (currentState)
        {
            case State.Chase:
                direction = toPlayer.normalized;
                break;
            case State.TooClose:
                direction = -toPlayer.normalized;
                break;
            case State.InRange:
                direction = Vector3.zero;
                break;
        }

        float verticalVelocity = isGrounded
            ? Mathf.Max(rb.linearVelocity.y, 0f)
            : rb.linearVelocity.y;

        Vector3 velocity = direction * moveSpeed;
        velocity.y = verticalVelocity;
        rb.linearVelocity = velocity;

        if (toPlayer.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(toPlayer);
    }

    private void HandleAttack()
    {
        if (currentState == State.InRange && attackTimer <= 0f)
        {
            attackTimer = attackCooldown;
            SendMessage("Shoot", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            col.bounds.extents.y + groundRayLength,
            groundLayer
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRange);
    }
}