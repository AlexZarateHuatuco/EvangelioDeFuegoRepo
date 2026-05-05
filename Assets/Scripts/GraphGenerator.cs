using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator
{
    public List<RoomNode> Generate(int mainLength = 30)
    {
        List<RoomNode> nodes = new();

        for (int i = 0; i < mainLength; i++)
        {
            RoomNode n = new RoomNode();
            n.id = i;
            n.isMainPath = true;

            n.type = Random.value > 0.4f ? RoomType.Presion : RoomType.Control;

            if (i < mainLength * 0.3f) n.zone = ZoneType.Iglesia;
            else if (i < mainLength * 0.7f) n.zone = ZoneType.Cementerio;
            else n.zone = ZoneType.Boss;

            nodes.Add(n);

            if (i > 0)
            {
                nodes[i - 1].connections.Add(n);
                n.connections.Add(nodes[i - 1]);
            }
        }

        AddBranches(nodes);

        return nodes;
    }

    void AddBranches(List<RoomNode> nodes)
    {
        int branchCount = nodes.Count / 4;

        for (int i = 0; i < branchCount; i++)
        {
            RoomNode baseNode = nodes[Random.Range(2, nodes.Count - 3)];

            int branchLength = Random.Range(2, 5);

            RoomNode prev = baseNode;

            for (int j = 0; j < branchLength; j++)
            {
                RoomNode branch = new RoomNode();
                branch.id = nodes.Count;
                branch.isMainPath = false;

                branch.type = RoomType.Control;
                branch.zone = baseNode.zone;

                prev.connections.Add(branch);
                branch.connections.Add(prev);

                nodes.Add(branch);
                prev = branch;
            }
        }
    }
}