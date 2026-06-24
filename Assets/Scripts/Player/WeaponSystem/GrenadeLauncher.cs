using System.Collections;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private Camera playerCamera;

    [Header("Estadísticas")]
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private float damage = 80f;
    [SerializeField] private int maxGrenades = 2;
    [SerializeField] private float rechargeTime = 12f;

    [Header("Input")]
    [SerializeField] private KeyCode throwKey = KeyCode.G;

    private int currentGrenades;

    public int CurrentGrenades => currentGrenades;
    public int MaxGrenades => maxGrenades;

    public event System.Action OnGrenadesChanged;

    private void Start()
    {
        currentGrenades = maxGrenades;

        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(throwKey))
            TryThrow();
    }

    private void TryThrow()
    {
        if (currentGrenades <= 0) return;
        if (grenadePrefab == null) return;

        Vector3 spawnPos = throwPoint != null ? throwPoint.position : transform.position;
        Vector3 direction = GetThrowDirection();

        GameObject go = Instantiate(grenadePrefab, spawnPos, Quaternion.LookRotation(direction));

        Grenade grenade = go.GetComponent<Grenade>();
        if (grenade != null)
            grenade.damage = damage;

        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.linearVelocity = direction * throwForce;
        }

        currentGrenades--;
        OnGrenadesChanged?.Invoke();

        StartCoroutine(RechargeCoroutine());
    }

    private Vector3 GetThrowDirection()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit, 100f)
            ? hit.point
            : ray.GetPoint(50f);

        Vector3 spawnPos = throwPoint != null ? throwPoint.position : transform.position;
        return (targetPoint - spawnPos).normalized;
    }

    private IEnumerator RechargeCoroutine()
    {
        yield return new WaitForSeconds(rechargeTime);

        if (currentGrenades < maxGrenades)
        {
            currentGrenades++;
            OnGrenadesChanged?.Invoke();
        }
    }

    public bool AddGrenades(int amount)
    {
        if (currentGrenades >= maxGrenades) return false;

        currentGrenades = Mathf.Min(currentGrenades + amount, maxGrenades);
        OnGrenadesChanged?.Invoke();
        return true;
    }
}