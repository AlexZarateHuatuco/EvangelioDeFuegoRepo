using UnityEngine;
using System.Collections.Generic;

public class GameBootstrap : MonoBehaviour
{
    public LevelGenerator generator;
    public LevelPreview preview;
    public Transform player;

    void Start()
    {
        GraphGenerator g = new GraphGenerator();
        List<RoomNode> nodes = g.Generate(8);

        SmartGridPlacer placer = new SmartGridPlacer();
        placer.GeneratePositions(nodes);

        preview.nodes = nodes;

        generator.Generate(nodes);

        RoomNode start = nodes[0];

        Vector3 startPos = new Vector3(
            start.gridPos.x * generator.cellSize,
            1f,
            start.gridPos.y * generator.cellSize
        );

        player.position = startPos;
    }
}