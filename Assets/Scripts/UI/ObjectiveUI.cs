using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public TMP_Text objectiveText;

    public void SetObjective(string objective)
    {
        objectiveText.text = "OBJETIVO:\n" + objective;
    }
}