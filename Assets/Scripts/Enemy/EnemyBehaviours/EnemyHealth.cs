/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action OnEnemyDeath;
    public event Action OnEnemyDamaged;

    [SerializeField] private bool isMiniBoss;
    [SerializeField] private bool isBoss;

    [Header("Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [Header("Armor (solo jefes)")]
    [SerializeField] private bool hasArmor;
    [SerializeField] private float currentArmor;
    [SerializeField] private float maxArmor;

    [Header("Regeneration (solo jefes)")]
    [SerializeField] private bool canRegenerate;
    [SerializeField] private float regenThresholdPercent;
    [SerializeField] private float regenAmountPerTick;
    [SerializeField] private float regenTickInterval;
    [SerializeField] private float regenDuration;

    [Header("Vulnerable To")]
    [SerializeField] private List<BulletType> vulnerableTo = new List<BulletType>();

    public bool IsBoss => isBoss;
    public bool IsMiniBoss => isMiniBoss;

    private bool isRegenerating = false;

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = hasArmor ? maxArmor : 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        BulletType type;
        float damage;

        BulletPlayer bullet = other.GetComponent<BulletPlayer>();
        if (bullet != null)
        {
            type = bullet.bulletType;
            damage = bullet.damage;
        }
        else
        {
            Grenade grenade = other.GetComponent<Grenade>();
            if (grenade == null) return;

            type = grenade.bulletType;
            damage = grenade.damage;
        }

        if (!vulnerableTo.Contains(type)) return;

        TakeDamage(damage);
        Destroy(other.gameObject);
    }

    public bool IsVulnerableTo(BulletType type)
    {
        return vulnerableTo.Contains(type);
    }

    public void TakeDamage(float damage)
    {
        if (hasArmor && currentArmor > 0f)
        {
            currentArmor -= damage;

            if (currentArmor <= 0f)
            {
                currentArmor = 0f;
                Debug.Log($"[{gameObject.name}] Armadura destruida.");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] Daño absorbido por armadura: {damage} | Armadura restante: {currentArmor}");
            }
        }
        else
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            Debug.Log($"[{gameObject.name}] Daño: {damage} | Vida restante: {currentHealth}");
        }

        OnEnemyDamaged?.Invoke();

        if (TutorialManager.Instance != null)
            TutorialManager.Instance.EnemyDamaged();

        CheckRegenThreshold();

        if (currentHealth <= 0f)
            Die();
    }

    private void CheckRegenThreshold()
    {
        if (!canRegenerate || isRegenerating) return;

        float healthPercent = (currentHealth / maxHealth) * 100f;
        float armorPercent = hasArmor ? (currentArmor / maxArmor) * 100f : 100f;

        if (healthPercent <= regenThresholdPercent || armorPercent <= regenThresholdPercent)
            StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        isRegenerating = true;
        Debug.Log($"[{gameObject.name}] Iniciando regeneración.");

        float elapsed = 0f;

        while (elapsed < regenDuration)
        {
            if (hasArmor && currentArmor < maxArmor)
            {
                currentArmor = Mathf.Min(currentArmor + regenAmountPerTick, maxArmor);
                Debug.Log($"[{gameObject.name}] Regenerando armadura: {currentArmor}");
            }
            else if (currentHealth < maxHealth)
            {
                currentHealth = Mathf.Min(currentHealth + regenAmountPerTick, maxHealth);
                Debug.Log($"[{gameObject.name}] Regenerando vida: {currentHealth}");
            }

            elapsed += regenTickInterval;
            yield return new WaitForSeconds(regenTickInterval);
        }

        isRegenerating = false;
        Debug.Log($"[{gameObject.name}] Regeneración terminada.");
    }

    public void Die()
    {
        Debug.Log($"[{gameObject.name}] Enemy died.");
        OnEnemyDeath?.Invoke();

        if (TutorialManager.Instance != null)
            TutorialManager.Instance.EnemyKilled();

        Destroy(gameObject);
    }

    public float GetHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public float GetArmor() => currentArmor;
    public float GetMaxArmor() => maxArmor;
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(EnemyHealth))]
public class EnemyHealthEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "vulnerableTo");

        var prop = serializedObject.FindProperty("vulnerableTo");

        UnityEditor.EditorGUILayout.Space();
        UnityEditor.EditorGUILayout.LabelField("Vulnerable To", UnityEditor.EditorStyles.boldLabel);

        var currentTypes = new List<BulletType>();
        for (int i = 0; i < prop.arraySize; i++)
            currentTypes.Add((BulletType)prop.GetArrayElementAtIndex(i).enumValueIndex);

        UnityEditor.EditorGUI.BeginChangeCheck();

        var newTypes = new List<BulletType>(currentTypes);

        foreach (BulletType type in System.Enum.GetValues(typeof(BulletType)))
        {
            bool isChecked = currentTypes.Contains(type);
            bool newChecked = UnityEditor.EditorGUILayout.Toggle(type.ToString(), isChecked);

            if (newChecked && !isChecked) newTypes.Add(type);
            else if (!newChecked && isChecked) newTypes.Remove(type);
        }

        if (UnityEditor.EditorGUI.EndChangeCheck())
        {
            prop.ClearArray();
            for (int i = 0; i < newTypes.Count; i++)
            {
                prop.InsertArrayElementAtIndex(i);
                prop.GetArrayElementAtIndex(i).enumValueIndex = (int)newTypes[i];
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
*/
using System;
using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Fire,
    Ice,
    Electric,
    Explosive,
    Poison,
    Light,
    Dark
}

public class EnemyHealth : MonoBehaviour
{
    public event Action OnEnemyDeath;
    public event Action OnEnemyDamaged;

    [SerializeField] private bool isMiniBoss;
    [SerializeField] private bool isBoss;

    [Header("Enemy Type")]
    [SerializeField] private EnemyType enemyType;

    [Header("Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [Header("Armor (solo jefes)")]
    [SerializeField] private bool hasArmor;
    [SerializeField] private float currentArmor;
    [SerializeField] private float maxArmor;

    [Header("Regeneration (solo jefes)")]
    [SerializeField] private bool canRegenerate;
    [SerializeField] private float regenThresholdPercent;
    [SerializeField] private float regenAmountPerTick;
    [SerializeField] private float regenTickInterval;
    [SerializeField] private float regenDuration;

    public bool IsBoss => isBoss;
    public bool IsMiniBoss => isMiniBoss;
    public EnemyType Type => enemyType;

    private bool isRegenerating = false;

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = hasArmor ? maxArmor : 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        BulletPlayer bullet = other.GetComponent<BulletPlayer>();

        if (bullet != null)
        {
            //if (!IsVulnerableTo(bullet._bulletType))
            //    return;

            TakeDamage(bullet.Damage);
            Destroy(other.gameObject);
            return;
        }

        Grenade grenade = other.GetComponent<Grenade>();

        if (grenade != null)
        {
            TakeDamage(grenade.damage);
        }
    }

    //public bool IsVulnerableTo(BulletType bulletType)
    //{
    //    if (bulletType == BulletType.Grenade)
    //        return true;

    //    return (EnemyType)bulletType == enemyType;
    //}

    public void TakeDamage(float damage)
    {
        if (hasArmor && currentArmor > 0f)
        {
            currentArmor -= damage;

            if (currentArmor <= 0f)
            {
                currentArmor = 0f;

                Debug.Log(
                    $"[{gameObject.name}] Armadura destruida."
                );
            }
            else
            {
                Debug.Log(
                    $"[{gameObject.name}] Daño absorbido por armadura: {damage} | Armadura restante: {currentArmor}"
                );
            }
        }
        else
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            Debug.Log(
                $"[{gameObject.name}] Daño: {damage} | Vida restante: {currentHealth}"
            );
        }

        OnEnemyDamaged?.Invoke();

        if (TutorialManager.Instance != null)
            TutorialManager.Instance.EnemyDamaged();

        CheckRegenThreshold();

        if (currentHealth <= 0f)
            Die();
    }

    private void CheckRegenThreshold()
    {
        if (!canRegenerate || isRegenerating)
            return;

        float healthPercent = (currentHealth / maxHealth) * 100f;

        float armorPercent = hasArmor
            ? (currentArmor / maxArmor) * 100f
            : 100f;

        if (healthPercent <= regenThresholdPercent ||
            armorPercent <= regenThresholdPercent)
        {
            StartCoroutine(Regenerate());
        }
    }

    private IEnumerator Regenerate()
    {
        isRegenerating = true;

        Debug.Log(
            $"[{gameObject.name}] Iniciando regeneración."
        );

        float elapsed = 0f;

        while (elapsed < regenDuration)
        {
            if (hasArmor && currentArmor < maxArmor)
            {
                currentArmor = Mathf.Min(
                    currentArmor + regenAmountPerTick,
                    maxArmor
                );

                Debug.Log(
                    $"[{gameObject.name}] Regenerando armadura: {currentArmor}"
                );
            }
            else if (currentHealth < maxHealth)
            {
                currentHealth = Mathf.Min(
                    currentHealth + regenAmountPerTick,
                    maxHealth
                );

                Debug.Log(
                    $"[{gameObject.name}] Regenerando vida: {currentHealth}"
                );
            }

            elapsed += regenTickInterval;

            yield return new WaitForSeconds(regenTickInterval);
        }

        isRegenerating = false;

        Debug.Log(
            $"[{gameObject.name}] Regeneración terminada."
        );
    }

    public void Die()
    {
        Debug.Log(
            $"[{gameObject.name}] Enemy died."
        );

        OnEnemyDeath?.Invoke();

        if (TutorialManager.Instance != null)
            TutorialManager.Instance.EnemyKilled();

        Destroy(gameObject);
    }

    public float GetHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;

    public float GetArmor() => currentArmor;
    public float GetMaxArmor() => maxArmor;
}