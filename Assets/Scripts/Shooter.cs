using UnityEngine;

public class EnemyShooter2D : MonoBehaviour
{
    [Header("Shooting Parameters")]
    public float shootDistance = 10f;      // Maximum shooting distance
    public float bulletSpeed = 5f;         // Speed of the bullet
    public float fireRate = 1f;            // Time between shots in seconds

    [Header("References")]
    public GameObject bulletPrefab;        // Prefab for the bullet
    public Transform target;               // The player's transform

    private float nextFireTime = 0f;       // Tracks when the enemy can fire next

    public Sprite shootSprite;     // The sprite to show when shooting
    public Sprite defaultSprite;   // The sprite to revert to after shooting
    public float spriteChangeDuration = 0.2f; // Duration to show the shoot sprite

    private SpriteRenderer spriteRenderer;

    public Animator anim;

    private Vector3 lastPosition;   
    private float horizontalSpeed;
    private float verticalSpeed;

    public float HorizontalSpeed => horizontalSpeed;
    public float VerticalSpeed => verticalSpeed;
    public float TotalSpeed { get; private set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the enemy object!");
        }
    }

    private void Update()
    {
        Vector3 movementDelta = transform.position - lastPosition;

        // Calculate speeds
        float hSpeed = CalculateHorizontalSpeed(movementDelta);
        float vSpeed = CalculateVerticalSpeed(movementDelta);

        // Calculate total speed
        float totalSpeed = movementDelta.magnitude / Time.deltaTime;

        // Update animator parameters
        anim.SetFloat("Horizontal", hSpeed);
        anim.SetFloat("Vertical", vSpeed);
        anim.SetFloat("Speed", totalSpeed);

        // Update last position
        lastPosition = transform.position;

        Debug.Log($"Horizontal Speed: {hSpeed}, Vertical Speed: {vSpeed}, Total Speed: {totalSpeed}");

        // Check if target is assigned
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to EnemyShooter2D.");
            return;
        }

        // Calculate distance to target
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Check if target is within shooting range
        if (distanceToTarget <= shootDistance)
        {
            // Calculate shooting direction
            Vector3 direction = (target.position - transform.position).normalized;

            // Draw a debug ray to visualize shooting direction
            Debug.DrawRay(transform.position, direction * shootDistance, Color.red);

            // Fire if the cooldown has elapsed
            if (Time.time >= nextFireTime)
            {
                Shoot(direction);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    // Method to assign the target dynamically
    public void AssignTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"Target assigned to {newTarget.name}");
    }

    public void Shoot(Vector3 direction)
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("BulletPrefab is not assigned.");
            return;
        }

        anim.SetTrigger("isShooting");

        // Instantiate bullet at enemy's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Get the Rigidbody2D component
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody2D component!");
            Destroy(bullet);
            return;
        }

        // Set bullet velocity
        rb.velocity = direction.normalized * bulletSpeed;

        Debug.Log($"Bullet spawned at {transform.position} with velocity {rb.velocity}");

        // Destroy the bullet after it travels the specified distance
        Destroy(bullet, shootDistance / bulletSpeed);
    }

    private void ResetSprite()
    {
        if (spriteRenderer != null && defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }

    float CalculateHorizontalSpeed(Vector3 movementDelta)
    {
        Vector3 horizontalMovement = new Vector3(movementDelta.x, 0, movementDelta.z);
        return horizontalMovement.magnitude / Time.deltaTime;
    }

    float CalculateVerticalSpeed(Vector3 movementDelta)
    {
        return movementDelta.y / Time.deltaTime;
    }
}