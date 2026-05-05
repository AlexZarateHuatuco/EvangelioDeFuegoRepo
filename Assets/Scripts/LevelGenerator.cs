using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public float cellSize = 10f;

    public void Generate(List<RoomNode> nodes)
    {
        foreach (var n in nodes)
        {
            Spawn(n);
        }
    }

    void Spawn(RoomNode node)
    {
        GameObject prefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

        Vector3 targetPos = new Vector3(
            node.gridPos.x * cellSize,
            0,
            node.gridPos.y * cellSize
        );

        GameObject go = Instantiate(prefab);

        RoomInstance inst = go.GetComponent<RoomInstance>();

        if (inst == null || inst.anchor == null)
        {
            Debug.LogError("Prefab sin Anchor configurado correctamente");
            go.transform.position = targetPos;
            return;
        }

        Vector3 offset = inst.anchor.position - go.transform.position;

        go.transform.position = targetPos - offset;

        node.instance = inst;
    }
}