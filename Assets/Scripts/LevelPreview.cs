using UnityEngine;
using System.Collections.Generic;

public class LevelPreview : MonoBehaviour
{
    public List<RoomNode> nodes;
    public float cellSize = 10f;

    void OnDrawGizmos()
    {
        if (nodes == null) return;

        foreach (var n in nodes)
        {
            Vector3 pos = new Vector3(n.gridPos.x * cellSize, 0, n.gridPos.y * cellSize);

            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(pos, Vector3.one * 1.5f);
        }
    }
}