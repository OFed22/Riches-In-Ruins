using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Contact Damage");
            // Try to get the player's health component
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Check if current time is past the next damage time
            if (Time.time >= nextDamageTime)
            {
                // Apply damage if health component exists
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    
                    // Set the next time damage can be applied
                    nextDamageTime = Time.time + damageCooldown;
                }
            }
        }
    }
}