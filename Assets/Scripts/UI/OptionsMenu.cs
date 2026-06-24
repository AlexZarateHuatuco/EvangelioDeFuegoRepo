using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public PlayerMovement playerMovement;

    [Header("Sensibilidad Horizontal")]
    public Slider horizontalSlider;
    public TMP_Text horizontalValueText;

    [Header("Sensibilidad Vertical")]
    public Slider verticalSlider;
    public TMP_Text verticalValueText;

    private const float minSensitivity = 10f;
    private const float maxSensitivity = 500f;

    private void Start()
    {
        horizontalSlider.minValue = 1;
        horizontalSlider.maxValue = 100;
        horizontalSlider.wholeNumbers = true;

        verticalSlider.minValue = 1;
        verticalSlider.maxValue = 100;
        verticalSlider.wholeNumbers = true;

        int savedHorizontal = PlayerPrefs.GetInt("HorizontalSliderValue", 40);
        int savedVertical = PlayerPrefs.GetInt("VerticalSliderValue", 40);

        horizontalSlider.value = savedHorizontal;
        verticalSlider.value = savedVertical;

        UpdateHorizontal(savedHorizontal);
        UpdateVertical(savedVertical);

        horizontalSlider.onValueChanged.AddListener(UpdateHorizontal);
        verticalSlider.onValueChanged.AddListener(UpdateVertical);
    }

    public void UpdateHorizontal(float sliderValue)
    {
        int displayValue = Mathf.RoundToInt(sliderValue);

        horizontalValueText.text = displayValue.ToString();

        float sensitivity =
            Mathf.Lerp(minSensitivity,
                        maxSensitivity,
                        (displayValue - 1f) / 99f);

        playerMovement.horizontalSensitivity = sensitivity;

        PlayerPrefs.SetInt("HorizontalSliderValue", displayValue);
        PlayerPrefs.SetFloat("HorizontalSensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void UpdateVertical(float sliderValue)
    {
        int displayValue = Mathf.RoundToInt(sliderValue);

        verticalValueText.text = displayValue.ToString();

        float sensitivity =
            Mathf.Lerp(minSensitivity,
                        maxSensitivity,
                        (displayValue - 1f) / 99f);

        playerMovement.verticalSensitivity = sensitivity;

        PlayerPrefs.SetInt("VerticalSliderValue", displayValue);
        PlayerPrefs.SetFloat("VerticalSensitivity", sensitivity);
        PlayerPrefs.Save();
    }
}