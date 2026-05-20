using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialState triggerState;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (other.CompareTag("Player"))
        {
            activated = true;

            TutorialManager.Instance.SetState(triggerState);
        }
    }
}