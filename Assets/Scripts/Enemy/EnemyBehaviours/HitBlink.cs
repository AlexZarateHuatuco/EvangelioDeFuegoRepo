using System.Collections;
using UnityEngine;

public class HitBlink : MonoBehaviour
{
    [Header("Flash Settings")]
    [SerializeField] private float flashDuration = 0.08f;
    [SerializeField] private int flashCount = 2;
    [SerializeField] private Color flashColor = Color.white;

    private SpriteRenderer[] spriteRenderers;
    private Color[] originalColors;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        if (spriteRenderers.Length == 0)
        {
            Debug.LogWarning($"[HitBlink] No se encontraron SpriteRenderer en {gameObject.name}");
            return;
        }

        originalColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }
    }

    private void Start()
    {
        EnemyHealth health = GetComponent<EnemyHealth>();

        if (health != null)
            health.OnEnemyDamaged += TriggerFlash;
        else
            Debug.LogWarning($"[HitBlink] No se encontró EnemyHealth en {gameObject.name}");
    }

    private void OnDestroy()
    {
        EnemyHealth health = GetComponent<EnemyHealth>();

        if (health != null)
            health.OnEnemyDamaged -= TriggerFlash;
    }

    private void TriggerFlash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.color = flashColor;
            }

            yield return new WaitForSeconds(flashDuration);

            for (int j = 0; j < spriteRenderers.Length; j++)
            {
                spriteRenderers[j].color = originalColors[j];
            }

            yield return new WaitForSeconds(flashDuration);
        }

        flashCoroutine = null;
    }
}