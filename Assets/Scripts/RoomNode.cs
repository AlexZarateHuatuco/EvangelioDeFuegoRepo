using UnityEngine;
using System.Collections.Generic;

public enum RoomType { Control, Presion }
public enum ZoneType { Iglesia, Cementerio, Boss }

public class RoomNode
{
    public int id;
    public Vector2Int gridPos;

    public RoomType type;
    public ZoneType zone;

    public bool isMainPath;
    public bool isShortcut;

    public List<RoomNode> connections = new();

    public RoomInstance instance;
}