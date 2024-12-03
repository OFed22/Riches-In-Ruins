using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVelocity : MonoBehaviour
{
    
    public Animator anim;

    private Vector3 lastPosition;
    private float horizontalSpeed;
    private float verticalSpeed;

    public float HorizontalSpeed => horizontalSpeed;
    public float VerticalSpeed => verticalSpeed;
    public float TotalSpeed { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
        lastPosition = transform.position;
    }

    float CalculateHorizontalSpeed(Vector3 movementDelta)
    {
        // For 3D (Z-axis forward)
        Vector3 horizontalMovement = new Vector3(movementDelta.x, 0, movementDelta.z);
        return horizontalMovement.magnitude / Time.deltaTime;
    }

    float CalculateVerticalSpeed(Vector3 movementDelta)
    {
        // For 3D (Z-axis forward)
        return movementDelta.y / Time.deltaTime;
    }

    // Optional method for 2D games (Y-axis forward)
    float CalculateHorizontalSpeed2D(Vector3 movementDelta)
    {
        // For 2D (Y-axis forward)
        Vector3 horizontalMovement = new Vector3(movementDelta.x, movementDelta.y, 0);
        return horizontalMovement.magnitude / Time.deltaTime;
    }

    float CalculateVerticalSpeed2D(Vector3 movementDelta)
    {
        // For 2D (Y-axis forward)
        return movementDelta.z / Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 movementDelta = transform.position - lastPosition;

        // Calculate speeds
        float hSpeed = CalculateHorizontalSpeed(movementDelta);
        float vSpeed = CalculateVerticalSpeed(movementDelta);
        
        // Calculate total speed
        float TotalSpeed = movementDelta.magnitude / Time.deltaTime;

        // Update last position for next frame
        lastPosition = transform.position;
       
        //Debug.Log($"Horizontal Speed: {hSpeed}");
        //Debug.Log($"Vertical Speed: {vSpeed}");
        //Debug.Log($"Total Speed: {TotalSpeed}");

		anim.SetFloat("Horizontal", hSpeed);
        anim.SetFloat("Vertical", vSpeed);
        anim.SetFloat("Speed", TotalSpeed);
        
    }
}
