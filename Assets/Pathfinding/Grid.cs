using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    public bool onlyDisplayPathGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    [Range(0.1f, 100)]
    public List<Node> path;
    public bool updateGrid;
    public GameSettings settings;

    Node[,] grid;
    float nodeRadius;
    float nodeDiameter { get { return nodeRadius * 2; } }
    int gridSizeX { get { return Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); } }
    int gridSizeY { get { return Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); } }

    void Awake()
    {
        nodeRadius = settings.GridSize / 2;
    }
    void Start()
    {
        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    public void CreateGrid()
    {
        try
        {
            grid = new Node[gridSizeX, gridSizeY];
        }
        catch (System.OverflowException)
        {
            print("X: " + gridSizeX + "Y:" + gridSizeY);
            throw;
        }
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                _AddNodeToGrid(worldBottomLeft, x, y);
            }
        }
    }

    void _AddNodeToGrid(Vector3 worldBottomLeft, int x, int y)
    {
        Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
        bool walkable = (Physics.CheckSphere(worldPoint, nodeRadius - 0.1f, unwalkableMask));
        grid[x, y] = new Node(walkable, worldPoint, x, y);
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (onlyDisplayPathGizmos)
        {
            if (path != null)
            {
                foreach (Node n in path)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .2f));
                }
            }
        }
        else
        {
            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    if (n.walkable)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - .05f));
                    }

                    if (path != null && path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .2f));
                    }
                }
            }
        }
    }

    public void OnValidate()
    {
        if (gridWorldSize.x < 1)
        {
            gridWorldSize.x = 1;
        }

        if (gridWorldSize.y < 1)
        {
            gridWorldSize.y = 1;
        }

        CreateGrid();
    }
}