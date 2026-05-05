using UnityEngine;

public enum Dir { N, S, E, O }

public class RoomInstance : MonoBehaviour
{
    public Transform anchor;

    public Connector north;
    public Connector south;
    public Connector east;
    public Connector west;

    public Connector Get(Dir d)
    {
        return d switch
        {
            Dir.N => north,
            Dir.S => south,
            Dir.E => east,
            Dir.O => west,
            _ => null
        };
    }

    public void ApplyRotation(int rotationSteps)
    {
        transform.rotation = Quaternion.Euler(0, rotationSteps * 90f, 0);
    }
}