using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGray2 : MonoBehaviour
{
    public LayerMask UnwalkableMask;
    public Vector2 GridWorldSize;
    public float NodeRadius;
    Node[,] grid;

    float NodeDiameter;
    int GridSizeX, GridSizeY;

    public static List<Node> PatrolGray2;

    void Awake()
    {
        NodeDiameter = NodeRadius * 2;
        GridSizeX = Mathf.RoundToInt(GridWorldSize.x / NodeDiameter);
        GridSizeY = Mathf.RoundToInt(GridWorldSize.y / NodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[GridSizeX, GridSizeY];
        Vector3 WorldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.up * GridWorldSize.y / 2;

        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 WorldPoint = WorldBottomLeft + Vector3.right * (x * NodeDiameter + NodeRadius) + Vector3.up * (y * NodeDiameter + NodeRadius);
                bool Walkable = !(Physics.CheckSphere(WorldPoint, NodeRadius, UnwalkableMask));
                grid[x, y] = new Node(Walkable, WorldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        int CheckX;
        int CheckY;

        CheckX = node.gridX + 1;
        CheckY = node.gridY;
        if (CheckX >= 0 && CheckX < GridSizeX)
        {
            if (CheckY >= 0 && CheckY < GridSizeY)
            {
                neighbours.Add(grid[CheckX, CheckY]);
            }
        }

        CheckX = node.gridX - 1;
        CheckY = node.gridY;
        if (CheckX >= 0 && CheckX < GridSizeX)
        {
            if (CheckY >= 0 && CheckY < GridSizeY)
            {
                neighbours.Add(grid[CheckX, CheckY]);
            }
        }

        CheckX = node.gridX;
        CheckY = node.gridY + 1;
        if (CheckX >= 0 && CheckX < GridSizeX)
        {
            if (CheckY >= 0 && CheckY < GridSizeY)
            {
                neighbours.Add(grid[CheckX, CheckY]);
            }
        }
        CheckX = node.gridX;
        CheckY = node.gridY - 1;
        if (CheckX >= 0 && CheckX < GridSizeX)
        {
            if (CheckY >= 0 && CheckY < GridSizeY)
            {
                neighbours.Add(grid[CheckX, CheckY]);
            }
        }
        return neighbours;
    }

    public virtual Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (worldPosition.y + GridWorldSize.y / 2) / GridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);
        return grid[x, y];
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, GridWorldSize.y, 1));
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if (PatrolGray2 != null)
                {
                    if (PatrolGray2.Contains(node))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (NodeDiameter - .1f));
            }
        }
    }
}
