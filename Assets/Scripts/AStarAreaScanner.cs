using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class AStarAreaScanner : MonoBehaviour
{
    public static AStarAreaScanner Instance { get; private set; }

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reference to the A* grid
    private GridGraph gridGraph;

    // List to store walkable nodes
    private List<GraphNode> walkableNodes = new List<GraphNode>();

    // Flag to ensure scanning happens only once
    private bool hasScanned = false;

    void Start()
    {
        // Get the first grid graph in the scene
        gridGraph = AstarPath.active.data.gridGraph;

        // Scan the area once on start
        ScanArea();
    }

    // Method to scan the entire area and collect walkable nodes
    public void ScanArea()
    {
        // Clear previous walkable nodes and any other cached state
        walkableNodes.Clear();

        // Check if grid graph exists
        if (gridGraph == null)
        {
            Debug.LogError("Grid Graph not found! Ensure A* Pathfinding Project is set up correctly.");
            return;
        }

        // Force a complete scan of the graph
        AstarPath.active.Scan();

        // Iterate through all nodes in the grid
        foreach (GraphNode node in gridGraph.nodes)
        {
            // Check if the node is walkable and add it to the list
            if (node.Walkable)
            {
                walkableNodes.Add(node);
            }
        }

        // Debug log for number of walkable nodes found
        Debug.Log($"Scanned area. Found {walkableNodes.Count} walkable nodes.");
    }

    // Method to get a random walkable tile
    public Vector3 GetRandomWalkableTile()
    {

        // Ensure we have walkable nodes
        if (walkableNodes.Count == 0)
        {
            Debug.LogError("No walkable nodes found!");
            return Vector3.zero;
        }

        // Select a random walkable node
        GraphNode randomNode = walkableNodes[Random.Range(0, walkableNodes.Count)];

        // Convert node to world position
        return (Vector3)randomNode.position;
    }

    public void FloodFillEnclosedArea(Vector3 startPosition)
    {
        if (gridGraph == null)
        {
            Debug.LogError("Grid Graph not found!");
            return;
        }

        List<GraphNode> enclosedNodes = new List<GraphNode>();
        GraphNode startNode = gridGraph.GetNearest(startPosition).node;

        Queue<GraphNode> queue = new Queue<GraphNode>();
        HashSet<GraphNode> visited = new HashSet<GraphNode>();

        queue.Enqueue(startNode);
        visited.Add(startNode);

        while (queue.Count > 0)
        {
            GraphNode currentNode = queue.Dequeue();

            if (currentNode.Walkable)
            {
                enclosedNodes.Add(currentNode);

                // Iterate through neighboring nodes
                currentNode.GetConnections(neighbor =>
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                });
            }
        }

        walkableNodes = enclosedNodes;

        Debug.Log($"Flood fill completed. Found {walkableNodes.Count} nodes in the enclosed area.");
    }

    // Optional method to visualize walkable nodes in the scene view
    void OnDrawGizmosSelected()
    {
        if (walkableNodes == null || walkableNodes.Count == 0)
            return;

        Gizmos.color = Color.green;
        foreach (GraphNode node in walkableNodes)
        {
            Gizmos.DrawWireCube((Vector3)node.position, Vector3.one * 0.5f);
        }
    }
}