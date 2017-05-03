using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{
    public bool onlyDisplayPathGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public List<Node> path;
    public bool updateGrid;
    public GameSettings settings;

    Node[,] grid;
    float nodeRadius;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    void Awake()
    {
        nodeRadius = settings.GridSize / 2;
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
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
        var sizeX = gridSizeX;
        var sizeY = gridSizeY;

        if (sizeX <= 0) sizeX = 1;
        if (sizeY <= 0) sizeY = 1;

        grid = new Node[sizeX, sizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
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

        int x = node.gridX;
        int y = node.gridY;

        int maxx = (int)gridWorldSize.x;
        int maxy = (int)gridWorldSize.y;

        neighbours.Add(grid[x + 1, y]);
        neighbours.Add(grid[x - 1, y]);
        neighbours.Add(grid[x, y + 1]);
        neighbours.Add(grid[x, y + 1]);

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

    bool _IsCardinalNeighbour(int thisX, int thisY, int neighbourX, int neighbourY)
    {
        var diff = new Vector2(neighbourX, neighbourY) - new Vector2(thisX, thisY);

        if (diff == new Vector2(1, 0)) return true;
        if (diff == new Vector2(-1, 0)) return true;
        if (diff == new Vector2(0, 1)) return true;
        if (diff == new Vector2(0, -1)) return true;

        return false;
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