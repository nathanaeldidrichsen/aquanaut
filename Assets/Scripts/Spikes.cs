using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 2; // Damage inflicted by the spikes

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Get the Health component of the player and inflict damage
            Health playerHealth = collision.collider.GetComponent<Health>();
            if (playerHealth != null)
            {

                Debug.Log("Player got hurt");
                playerHealth.GetHurt(damage, transform.position);
            }
        }
    }
}
