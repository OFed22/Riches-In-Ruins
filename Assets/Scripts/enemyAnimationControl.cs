using UnityEngine;
using Pathfinding; // AILerp namespace

public class enemyAnimationControl : MonoBehaviour
{
    private Animator animator;
    private AILerp aiLerp;

    void Start()
    {
        animator = GetComponent<Animator>();
        aiLerp = GetComponent<AILerp>();
    }

    void Update()
    {
        // Get the velocity of the AILerp
        Vector3 velocity = aiLerp.velocity;
        float speed = velocity.magnitude;

        // Normalize velocity for direction calculation
        Vector3 normalized = velocity.normalized;

        // Update Animator parameters
        animator.SetFloat("Speed", speed);
        animator.SetFloat("Horizontal", normalized.x);
        animator.SetFloat("Vertical", normalized.y);
    }
}
