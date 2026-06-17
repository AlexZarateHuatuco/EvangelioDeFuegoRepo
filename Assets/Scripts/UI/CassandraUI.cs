using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CassandraUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TMP_Text speakerName;
    public TMP_Text dialogueText;

    [Header("Configuración")]
    public float textSpeed = 0.03f;
    public float messageDelay = 2f;

    public bool IsTalking { get; private set; }

    public event Action OnDialogueFinished;

    private Queue<DialogueData> dialogueQueue =
        new Queue<DialogueData>();

    private Coroutine dialogueRoutine;

    private class DialogueData
    {
        public string speaker;
        public string[] messages;
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    public void ShowDialogue(
        string speaker,
        string message1
    )
    {
        EnqueueDialogue(
            speaker,
            new string[]
            {
                message1
            }
        );
    }

    public void ShowDialogue(
        string speaker,
        string message1,
        string message2
    )
    {
        EnqueueDialogue(
            speaker,
            new string[]
            {
                message1,
                message2
            }
        );
    }

    public void ShowDialogue(
        string speaker,
        string message1,
        string message2,
        string message3
    )
    {
        EnqueueDialogue(
            speaker,
            new string[]
            {
                message1,
                message2,
                message3
            }
        );
    }

    private void EnqueueDialogue(
        string speaker,
        string[] messages
    )
    {
        DialogueData data = new DialogueData();

        data.speaker = speaker;
        data.messages = messages;

        dialogueQueue.Enqueue(data);

        if (!IsTalking)
        {
            dialogueRoutine =
                StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        IsTalking = true;

        panel.SetActive(true);

        while (dialogueQueue.Count > 0)
        {
            DialogueData dialogue =
                dialogueQueue.Dequeue();

            speakerName.text =
                dialogue.speaker;

            foreach (string message in dialogue.messages)
            {
                yield return StartCoroutine(
                    TypeText(message)
                );

                yield return new WaitForSeconds(
                    messageDelay
                );
            }
        }

        panel.SetActive(false);

        IsTalking = false;

        OnDialogueFinished?.Invoke();
    }

    private IEnumerator TypeText(
        string message
    )
    {
        dialogueText.text = "";

        foreach (char c in message)
        {
            dialogueText.text += c;

            yield return new WaitForSeconds(
                textSpeed
            );
        }
    }

    public void ClearQueue()
    {
        dialogueQueue.Clear();

        if (dialogueRoutine != null)
        {
            StopCoroutine(dialogueRoutine);
        }

        dialogueText.text = "";

        panel.SetActive(false);

        IsTalking = false;
    }
}