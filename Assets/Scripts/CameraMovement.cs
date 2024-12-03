using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Reference to the player's transform

    [Header("Camera Movement")]
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement
    public float zPosition = -10f; // Fixed Z position for the camera

    void LateUpdate()
    {
        // If no target is set, try to find the player
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
            
            // If still no target, exit the method
            if (target == null) return;
        }

        // Calculate desired position, maintaining a fixed Z
        Vector3 desiredPosition = new Vector3(
            target.position.x, 
            target.position.y, 
            zPosition
        );

        // Smoothly interpolate between current position and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Explicitly set the Z position to the fixed value
        smoothedPosition.z = zPosition;

        // Update camera position
        transform.position = smoothedPosition;
    }
}