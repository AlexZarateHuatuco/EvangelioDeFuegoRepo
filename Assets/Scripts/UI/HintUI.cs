using UnityEngine;
using TMPro;
using System.Collections;

public class HintUI : MonoBehaviour
{
    public TMP_Text hintText;

    private Coroutine currentRoutine;

    public void ShowHint(string message, float duration)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(HintRoutine(message, duration));
    }

    IEnumerator HintRoutine(string message, float duration)
    {
        hintText.gameObject.SetActive(true);

        hintText.text = message;

        yield return new WaitForSeconds(duration);

        hintText.gameObject.SetActive(false);
    }
}