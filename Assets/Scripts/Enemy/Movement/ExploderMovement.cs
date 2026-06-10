using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExploderMovement : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 2.5f;

    [Header("Detección")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float triggerRange = 1.4f;
    [SerializeField] private float cancelRange = 2.5f;

    [Header("Explosión")]
    [SerializeField] private float fuseTime = 1.5f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionDamage = 50f;
    [SerializeField] private LayerMask damageLayers;
    [SerializeField] private GameObject explosionVFX;

    [Header("Feedback visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color armingColor = Color.red;

    [Header("Detección de piso")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRayLength = 0.2f;

    private Rigidbody rb;
    private Collider col;          // ← cacheado
    private bool isGrounded;

    private enum State { Idle, Chase, Arming, Dead }
    private State currentState = State.Idle;

    private float fuseTimer;
    private bool exploding;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>(); // ← cachear aquí

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null || currentState == State.Dead) return;

        float dist = Vector3.Distance(transform.position, player.position);
        EvaluateState(dist);
        HandleFuse(dist);
    }

    private void FixedUpdate()
    {
        if (player == null || currentState == State.Dead) return;
        HandleMovement();
    }

    private void EvaluateState(float dist)
    {
        switch (currentState)
        {
            case State.Idle:
                if (dist <= detectionRange) currentState = State.Chase;
                break;
            case State.Chase:
                if (dist <= triggerRange) EnterArming();
                break;
            case State.Arming:
                if (dist > cancelRange) CancelArming();
                break;
        }
    }

    private void EnterArming()
    {
        currentState = State.Arming;
        fuseTimer = fuseTime;
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private void CancelArming()
    {
        currentState = State.Chase;
        fuseTimer = 0f;
        if (flashCoroutine != null) { StopCoroutine(flashCoroutine); flashCoroutine = null; }
        if (spriteRenderer != null) spriteRenderer.color = normalColor;
    }

    private void HandleFuse(float dist)
    {
        if (currentState != State.Arming || exploding) return;

        fuseTimer -= Time.deltaTime;
        if (fuseTimer <= 0f) StartCoroutine(ExplodeRoutine());
    }

    private IEnumerator ExplodeRoutine()
    {
        exploding = true;
        currentState = State.Dead;

        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        if (spriteRenderer != null) spriteRenderer.color = armingColor;

        yield return new WaitForSeconds(0.1f);

        if (explosionVFX != null)
            Instantiate(explosionVFX, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, damageLayers);
        foreach (var hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            damageable?.TakeDamage(explosionDamage);
        }

        Destroy(gameObject);
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

        // ← Clamp Y al tocar el suelo para evitar acumulación negativa
        float verticalVelocity = isGrounded
            ? Mathf.Max(rb.linearVelocity.y, 0f)
            : rb.linearVelocity.y;

        Vector3 velocity = direction * moveSpeed;
        velocity.y = verticalVelocity;
        rb.linearVelocity = velocity;

        if (direction.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    private IEnumerator FlashRoutine()
    {
        if (spriteRenderer == null) yield break;

        while (currentState == State.Arming)
        {
            float t = Mathf.Clamp01(fuseTimer / fuseTime);
            float interval = Mathf.Lerp(0.08f, 0.4f, t);
            spriteRenderer.color = armingColor;
            yield return new WaitForSeconds(interval * 0.5f);
            spriteRenderer.color = normalColor;
            yield return new WaitForSeconds(interval * 0.5f);
        }
    }

    private void CheckGrounded()
    {
        // ← Usar col cacheado
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
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
        Gizmos.color = new Color(1f, 0.5f, 0f);
        Gizmos.DrawWireSphere(transform.position, cancelRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

public interface IDamageable
{
    void TakeDamage(float amount);
}