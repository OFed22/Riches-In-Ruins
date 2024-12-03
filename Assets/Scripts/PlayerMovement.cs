using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public AudioSource step;
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer; // Add this in the inspector
    public float collisionCheckDistance = 0.1f; // Small distance to check for obstacles

    [Header("Component References")]
    public Animator animator;
    private Rigidbody2D rb;

    private Vector2 movement;
    private bool isPlayingWalkingSound = false; // To track if the coroutine is running

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Input
    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement
        movement = movement.normalized;

        // Update animator parameters
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Start walking sound coroutine if moving
        if (movement.sqrMagnitude > 0 && !isPlayingWalkingSound)
        {
            StartCoroutine(PlayWalkingSound());
        }
    }
        
    // Movement
    void FixedUpdate()
    {
        // Check for obstacles in movement direction
        if (CanMove(movement))
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Optional: Stop movement if obstacle detected
            movement = Vector2.zero;
            animator.SetFloat("Speed", 0);
        }
    }

    // Check if movement is possible without hitting obstacles
    bool CanMove(Vector2 movementDirection)
    {
        // If no movement, always return true
        if (movementDirection == Vector2.zero)
            return true;

        // Perform raycast to check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(
            rb.position, 
            movementDirection, 
            collisionCheckDistance, 
            obstacleLayer
        );

        // Return true if no obstacle is detected
        return hit.collider == null;
    }

    public void IncreaseSpeed(float speed)
    {
        this.moveSpeed += speed;
    }

    // Coroutine to play walking sound every 0.2 seconds
    private IEnumerator PlayWalkingSound()
    {
        isPlayingWalkingSound = true;

        while (movement.sqrMagnitude > 0)
        {
            step.Play();
            yield return new WaitForSeconds(0.2f);
        }

        isPlayingWalkingSound = false;
    }

    // Optional: Visualize obstacle detection in scene views
    void OnDrawGizmosSelected()
    {
        if (rb == null) return;

        Gizmos.color = Color.red;
        Vector2 checkDirection = movement != Vector2.zero ? movement : Vector2.down;
        Gizmos.DrawRay(rb.position, checkDirection * collisionCheckDistance);
    }
}
