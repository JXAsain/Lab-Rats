using UnityEngine;
using System.Collections; // Imports the System.Collections namespace

public class moving : MonoBehaviour
{

    public float moveForce = 10f; // Adjust in the Inspector
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void FixedUpdate() // Use FixedUpdate for physics operations
    {
        // Get horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a force vector
        Vector3 force = new Vector3(horizontalInput, 0f, verticalInput) * moveForce;

        // Apply force to the Rigidbody
        rb.AddForce(force);
    }
}
