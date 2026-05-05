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

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    [Header("Detección")]
    public float visionRange = 15f;
    public LayerMask obstacleMask;

    [Header("Movimiento")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;
    private bool isAlerted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        if (!isAlerted)
            DetectPlayer();

        if (isAlerted)
            Move();
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= visionRange)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            if (!Physics.Raycast(transform.position, dir, distance, obstacleMask))
                isAlerted = true;
        }
    }

    void Move()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
        }

        Vector3 moveDir = direction.normalized;
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public bool IsAlerted()
    {
        return isAlerted;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public void Alert()
    {
        isAlerted = true;
    }
}