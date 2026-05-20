using UnityEngine;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    //Referencias
    [SerializeField] private PlayerHealth player;
    [SerializeField] private GameObject heartPrefab;
    private float lastHealth;
    private List<HeartIcons> hearts = new List<HeartIcons>();

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        if (player == null)
            player = FindAnyObjectByType<PlayerHealth>();
        DrawHearts();
        lastHealth = player.CurrentHealth;
    }

    private void Update()
    {
        if (player.CurrentHealth != lastHealth)
        {
            DrawHearts();
            lastHealth = player.CurrentHealth;
        }
    }

    private void CreateHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, transform);
        HeartIcons heartComponent = newHeart.GetComponent<HeartIcons>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    private void DrawHearts()
    {
        ClearHearts();

        float maxHealthRemainder = player.MaxHealth % 2;
        int heartsToMake = (int)((player.MaxHealth / 2) + maxHealthRemainder);

        for (int i = 0; i < heartsToMake; i++)
            CreateHeart();

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(player.CurrentHealth - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    private void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts.Clear();
    }
}