using UnityEngine;
using Pathfinding;

public class RandomWalker : MonoBehaviour
{
    private AIDestinationSetter destinationSetter; // Reference to AIDestinationSetter
    private AIPath aiPath; // Reference to AIPath to check when the enemy has reached the destination
    private GameObject tempTarget; // Persistent temporary GameObject for the destination

    private void Start()
    {
        // Get references to components
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();

        // Check if components exist
        if (destinationSetter == null)
        {
            Debug.LogError("AIDestinationSetter component is missing on this GameObject!");
            return;
        }

        if (aiPath == null)
        {
            Debug.LogError("AIPath component is missing on this GameObject!");
            return;
        }

        // Create a persistent temporary target GameObject
        tempTarget = new GameObject("TempTarget");
        tempTarget.transform.position = transform.position; // Initialize at the current position
        destinationSetter.target = tempTarget.transform; // Assign to AIDestinationSetter

        // Set the first random destination
        SetRandomDestination();
    }

    private void Update()
    {
        if (destinationSetter == null || aiPath == null)
        {
            return; // Exit if components are missing
        }

        // Check if the AI has reached its current destination
        if (aiPath.reachedEndOfPath && !aiPath.pathPending)
        {
            // Set a new random destination
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomPosition;
        if (AStarAreaScanner.Instance != null)
            {
                randomPosition = AStarAreaScanner.Instance.GetRandomWalkableTile();
            }
            else {
                randomPosition = Vector3.zero;
            }

        if (randomPosition == Vector3.zero)
        {
            // Update the position of the persistent target
            tempTarget.transform.position = randomPosition;
        }
        else
        {
            Debug.LogWarning("No random walkable tile found!");
        }
    }

    private void OnDestroy()
    {
        // Clean up the temporary GameObject when this object is destroyed
        if (tempTarget != null)
        {
            Destroy(tempTarget);
        }
    }
}