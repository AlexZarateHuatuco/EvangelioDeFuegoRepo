using UnityEngine;
using System.Collections.Generic;

public class SmartGridPlacer
{
    private Dictionary<Vector2Int, RoomNode> occupied = new();

    private Vector2Int[] dirs =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.down
    };

    public void GeneratePositions(List<RoomNode> nodes)
    {
        occupied.Clear();

        nodes[0].gridPos = Vector2Int.zero;
        occupied[nodes[0].gridPos] = nodes[0];

        Vector2Int lastDir = Vector2Int.up;

        for (int i = 1; i < nodes.Count; i++)
        {
            lastDir = Place(nodes[i - 1], nodes[i], lastDir);
        }
    }

    private Vector2Int Place(RoomNode current, RoomNode next, Vector2Int lastDir)
    {
        List<Vector2Int> candidates = new();

        foreach (var dir in dirs)
        {
            Vector2Int pos = current.gridPos + dir;

            if (occupied.ContainsKey(pos)) continue;

            int score = 1;

            if (dir == lastDir) score += 3;

            if (dir == -lastDir) score -= 2;

            if (dir == Vector2Int.up) score += 2;

            score -= CountNeighbors(pos);

            for (int i = 0; i < Mathf.Max(score, 1); i++)
                candidates.Add(dir);
        }

        if (candidates.Count == 0)
        {
            Vector2Int forced = FindFreeSpot(current.gridPos);

            next.gridPos = forced;
            occupied[forced] = next;

            return Vector2Int.up;
        }

        Vector2Int chosen = candidates[Random.Range(0, candidates.Count)];
        Vector2Int final = current.gridPos + chosen;

        next.gridPos = final;
        occupied[final] = next;

        return chosen;
    }

    private Vector2Int FindFreeSpot(Vector2Int origin)
    {
        int radius = 1;

        while (radius < 30)
        {
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    Vector2Int candidate = origin + new Vector2Int(x, y);

                    if (!occupied.ContainsKey(candidate))
                        return candidate;
                }
            }
            radius++;
        }

        Debug.LogError("No free spot found!");
        return origin;
    }

    public void CreateLoops(List<RoomNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 2; j < nodes.Count; j++)
            {
                if (Random.value > 0.2f) continue;

                var a = nodes[i];
                var b = nodes[j];

                int gridDist = Manhattan(a.gridPos, b.gridPos);
                int graphDist = j - i;

                if (gridDist == 1 && graphDist >= 3)
                {
                    if (!a.connections.Contains(b))
                    {
                        a.connections.Add(b);
                        b.connections.Add(a);
                    }
                }
            }
        }
    }

    public void CreateShortcuts(List<RoomNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int j = i + 3; j < nodes.Count; j++)
            {
                if (Random.value > 0.15f) continue;

                var a = nodes[i];
                var b = nodes[j];

                int gridDist = Manhattan(a.gridPos, b.gridPos);
                int graphDist = j - i;

                if (gridDist == 1 && graphDist >= 4)
                {
                    if (!a.connections.Contains(b))
                    {
                        a.connections.Add(b);
                        b.connections.Add(a);

                        a.isShortcut = true;
                        b.isShortcut = true;
                    }
                }
            }
        }
    }

    private int CountNeighbors(Vector2Int pos)
    {
        int count = 0;

        foreach (var d in dirs)
            if (occupied.ContainsKey(pos + d)) count++;

        return count;
    }

    private int Manhattan(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}