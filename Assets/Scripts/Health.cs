using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    public int currentHealth; // Current health

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
    }

    // Method to decrease health by a specified amount
    public void GetHurt(int damageAmount)
    {
        currentHealth -= damageAmount; // Decrease current health by damage amount

        if (currentHealth <= 0)
        {
            Die(); // If health is zero or below, call the Die method
        }
    }

    // Method to destroy the object
    private void Die()
    {
        Destroy(gameObject); // Destroy the GameObject this script is attached to
        HUD.Instance.LostGame();
    }
}
