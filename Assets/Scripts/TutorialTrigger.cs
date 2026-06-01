/*using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TutorialTrigger : MonoBehaviour
{
    [Header("Tutorial")]
    public TutorialState tutorialState;

    [Header("Solo una vez")]
    public bool triggerOnce = true;

    private bool activated = false;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated && triggerOnce)
            return;

        if (!other.CompareTag("Player"))
            return;

        activated = true;

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.SetState(
                tutorialState
            );
        }
    }
}*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TutorialTrigger : MonoBehaviour
{
    [Header("Tutorial")]
    public TutorialState tutorialState;

    [Header("Opciones")]
    public bool triggerOnce = true;

    public bool waitUntilDialogueEnds = true;

    private bool activated = false;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (activated && triggerOnce)
            return;

        if (TutorialManager.Instance == null)
            return;

        if (TutorialManager.Instance.currentState ==
            tutorialState)
        {
            return;
        }

        if (waitUntilDialogueEnds &&
            TutorialManager.Instance.cassandraUI.IsTalking)
        {
            StartCoroutine(
                WaitAndTrigger()
            );

            return;
        }

        ActivateTrigger();
    }

    private IEnumerator WaitAndTrigger()
    {
        while (
            TutorialManager.Instance.cassandraUI.IsTalking
        )
        {
            yield return null;
        }

        ActivateTrigger();
    }

    private void ActivateTrigger()
    {
        if (activated && triggerOnce)
            return;

        activated = true;

        TutorialManager.Instance.SetState(
            tutorialState
        );
    }
}