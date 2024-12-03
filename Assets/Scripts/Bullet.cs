using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public AudioSource laser;
    public int damage = 1;
    public float lifetime = 5f; // Optional: destroy bullet after certain time

    private void Start()
    {
        laser.Play();
        // Automatically destroy bullet after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hit the player
        if (collision.CompareTag("Player"))
        {
            // Try to get the player's health component
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            
            // If player health component exists, apply damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destroy the bullet after hitting the player
            Destroy(gameObject);
        }
        // Destroy bullet if it hits anything else
        else
        {
            Destroy(gameObject);
        }
    }
}