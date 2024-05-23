using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    public float targetY; // The target y position to move towards
    public float speed = 1.0f; // Speed of movement

    private void Update()
    {
        // Get the current position of the spike
        Vector3 position = transform.position;

        // Calculate the new y position
        float newY = Mathf.MoveTowards(position.y, targetY, speed * Time.deltaTime);

        // Update the position of the spike
        transform.position = new Vector3(position.x, newY, position.z);
    }
}
