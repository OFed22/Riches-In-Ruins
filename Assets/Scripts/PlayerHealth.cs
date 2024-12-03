using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public AudioSource hit;
    
    
    public int maxHealth = 3;
    public float damageCooldown = 3f;
    public AudioSource deathSound;

    public int currentHealth;
    private bool isImmune = false;

    //public GameObject deathEffect; // Optional death effect prefab

    void Start()
    {
        // Initialize health to max health at the start of the game
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        // Check if the player is currently immune to damage
        if (isImmune)
            return;

        
        hit.Play();

        // Reduce health
        currentHealth -= amount;

        // Log damage for debugging
        Debug.Log($"Player took {amount} damage. Current HP: {currentHealth}");

        // Check if player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Start damage cooldown coroutine
            StartCoroutine(DamageCooldown());
        }
    }

    public void Heal(int hp) {
        if (currentHealth <= maxHealth) {
            currentHealth += hp;
        }
    }

    public void IncreaseMaxHp(int hp) {
        maxHealth += hp;
        currentHealth += hp;
    }

    private void Die()
    {
        deathSound.Play(); 
        StartCoroutine(DamageCooldown());
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator DamageCooldown()
    {
        Debug.Log("Damage cooldown coroutine started");

        // Set immunity
        isImmune = true;
        Debug.Log("Player is now immune");

        // Wait for the specified cooldown duration
        yield return new WaitForSeconds(damageCooldown);

        // Remove immunity
        isImmune = false;
        Debug.Log("Player immunity ended");
    }

}