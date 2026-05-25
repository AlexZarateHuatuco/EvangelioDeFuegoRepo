/* UnityEngine;
using TMPro;
using System.Collections;

public class CassandraUI : MonoBehaviour
{
    /public static CassandraUI Instance;

    [Header("UI")]
    public GameObject panel;

    public TMP_Text speakerName;
    public TMP_Text dialogueText;

    [Header("Configuración")]
    public float textSpeed = 0.02f;

    public float messageDelay = 2.5f;

    private Coroutine dialogueRoutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDialogue(
        string speaker,
        string message1
    )
    {
        string[] messages =
        {
            message1
        };

        StartDialogue(
            speaker,
            messages
        );
    }

    public void ShowDialogue(
        string speaker,
        string message1,
        string message2
    )
    {
        string[] messages =
        {
            message1,
            message2
        };

        StartDialogue(
            speaker,
            messages
        );
    }

    public void ShowDialogue(
        string speaker,
        string message1,
        string message2,
        string message3
    )
    {
        string[] messages =
        {
            message1,
            message2,
            message3
        };

        StartDialogue(
            speaker,
            messages
        );
    }

    void StartDialogue(
        string speaker,
        string[] messages
    )
    {
        panel.SetActive(true);

        speakerName.text = speaker;

        if (dialogueRoutine != null)
        {
            StopCoroutine(
                dialogueRoutine
            );
        }

        dialogueRoutine =
            StartCoroutine(
                DialogueSequence(messages)
            );
    }

    IEnumerator DialogueSequence(
        string[] messages
    )
    {
        for (int i = 0; i < messages.Length; i++)
        {
            yield return StartCoroutine(
                TypeText(messages[i])
            );

            yield return new WaitForSeconds(
                messageDelay
            );
        }

        HideDialogue();
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;

            yield return new WaitForSeconds(
                textSpeed
            );
        }
    }

    public void HideDialogue()
    {
        panel.SetActive(false);
    }
}*/