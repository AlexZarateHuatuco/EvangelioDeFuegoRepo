using UnityEngine;

public enum ConnectorState { Open, Closed, Locked }
public enum ConnectorRole { Normal, Shortcut, Gate }

public class Connector : MonoBehaviour
{
    public ConnectorRole role;
    public ConnectorState state;

    public GameObject door;
    public Collider blocker;

    void Start()
    {
        if (role == ConnectorRole.Normal) SetState(ConnectorState.Open);
        if (role == ConnectorRole.Gate) SetState(ConnectorState.Closed);
        if (role == ConnectorRole.Shortcut) SetState(ConnectorState.Locked);
    }

    public void SetState(ConnectorState s)
    {
        state = s;

        bool open = s == ConnectorState.Open;

        if (door) door.SetActive(!open);
        if (blocker) blocker.enabled = !open;
    }

    public void OnRoomCleared()
    {
        if (role == ConnectorRole.Gate)
            SetState(ConnectorState.Open);
    }

    public void UnlockShortcut()
    {
        if (role == ConnectorRole.Shortcut)
            SetState(ConnectorState.Open);
    }
}