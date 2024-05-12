using UnityEngine;

public class AirPocket : MonoBehaviour
{
    public int oxygenReplenishAmount = 5; // Amount of oxygen replenished per second

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the Oxygen component of the player and replenish oxygen
            Oxygen playerOxygen = other.GetComponent<Oxygen>();
            if (playerOxygen != null)
            {
                playerOxygen.currentOxygenLevel += oxygenReplenishAmount;
                // Clamp the current oxygen level to the maximum oxygen level
                playerOxygen.currentOxygenLevel = Mathf.Clamp(playerOxygen.currentOxygenLevel, 0, playerOxygen.maxOxygenLevel);
            }
        }
    }
}
