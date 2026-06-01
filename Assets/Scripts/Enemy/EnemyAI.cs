/*using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float visionRange = 15f;
    public float attackRange = 2.5f;
    public LayerMask obstacleMask;

    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;
    private bool isAlerted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("No se encontró el Player");
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if (!isAlerted)
        {
            DetectPlayer();
        }

        if (isAlerted)
        {
            UpdateMovement();
        }
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= visionRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            if (!Physics.Raycast(transform.position, dir, distance, obstacleMask))
            {
                isAlerted = true;
            }
        }
    }

    void UpdateMovement()
    {
        //Recalcular posición
        Vector3 currentPlayerPos = player.position;

        Vector3 direction = currentPlayerPos - transform.position;
        direction.y = 0;

        float distance = direction.magnitude;

        //Rotación continua
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
        }

        //Movimiento continuo
        if (distance > attackRange)
        {
            Vector3 moveDir = direction.normalized;
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Atacando");
    }

    public void Alert()
    {
        isAlerted = true;
    }
}*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Detection")]
    public float visionRange = 15f;
    public LayerMask obstacleMask;

    [Header("Combat")]
    public float attackRange = 2.5f;
    public float attackAngle = 20f;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Tooltip("Grados por segundo")]
    public float rotationSpeed = 90f;

    private Rigidbody rb;
    private bool isAlerted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogWarning("No se encontró el Player");
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        if (!isAlerted)
            DetectPlayer();

        if (isAlerted)
            UpdateMovement();
    }

    private void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= visionRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            if (!Physics.Raycast(transform.position, dir, distance, obstacleMask))
            {
                isAlerted = true;
            }
        }
    }

    private void UpdateMovement()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        Quaternion newRotation = Quaternion.RotateTowards(
            rb.rotation,
            targetRotation,
            rotationSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(newRotation);

        float angleToPlayer = Vector3.Angle(transform.forward, direction);

        bool facingPlayer = angleToPlayer <= attackAngle;

        if (distance > attackRange)
        {
            if (facingPlayer)
            {
                Vector3 moveDir = direction.normalized;

                rb.MovePosition(
                    rb.position +
                    moveDir * moveSpeed * Time.fixedDeltaTime
                );
            }
        }
        else
        {
            if (facingPlayer)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Debug.Log("Atacando");
    }

    public void Alert()
    {
        isAlerted = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif
}