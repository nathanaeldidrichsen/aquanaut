using UnityEngine;

public class Oxygen : MonoBehaviour
{
    public int maxOxygenLevel = 100; // Maximum oxygen level
    public int currentOxygenLevel; // Current oxygen level
    public int oxygenDepletionRate = 1; // Oxygen depletion rate per second

    private void Start()
    {
        currentOxygenLevel = maxOxygenLevel; // Initialize current oxygen level to maximum oxygen level
        InvokeRepeating("DepleteOxygen", 1f, 1f); // Start depleting oxygen every second
    }

    // Method to deplete oxygen every second
    private void DepleteOxygen()
    {
        currentOxygenLevel -= oxygenDepletionRate; // Decrease current oxygen level by 1 every second

        if (currentOxygenLevel <= 0)
        {
            Die(); // If oxygen level is zero or below, call the Die method
        }
    }

    // Method to handle what happens when oxygen runs out
    private void Die()
    {
        HUD.Instance.LostGame();
        // Implement oxygen depletion behavior here
        Debug.Log("Out of oxygen!");
        // For example, you might want to call a method to end the game or trigger some other action
    }
}
