using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public static WaveUI Instance;

    [Header("UI")]
    public TMP_Text waveText;
    public TMP_Text enemiesText;
    public TMP_Text restText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateWave(int currentWave, int totalWaves)
    {
        if (waveText != null)
            waveText.text = $"Oleada: {currentWave}/{totalWaves}";
    }

    public void UpdateEnemies(int enemiesLeft)
    {
        if (enemiesText != null)
            enemiesText.text = $"Enemigos: {enemiesLeft}";
    }

    public void UpdateRest(float timeLeft)
    {
        if (restText != null)
            restText.text = $"Descanso: {Mathf.Ceil(timeLeft)}s";
    }

    public void HideRest()
    {
        if (restText != null)
            restText.text = "";
    }
}