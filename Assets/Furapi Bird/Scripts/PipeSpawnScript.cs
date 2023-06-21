using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce = 10; // Public float variable to hold the force of the player's jump
    public float moveSpeed = 10; // Public float variable to hold the speed at which the player moves
    public float deathZone = -10; // Public float variable to hold the position at which the player dies
    private Rigidbody2D rb; // Private reference to the player's Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the player's Rigidbody2D component and assign it to the rb variable
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deathZone) // Check if the player has fallen below the deathZone position
        {
            Debug.Log("Player Died."); // Log a message to the console
            Destroy(gameObject); // Destroy the player object
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Check if the spacebar has been pressed
        {
            rb.velocity = Vector2.up * jumpForce; // Add an upward force to the player's Rigidbody2D component
        }

        float horizontalInput = Input.GetAxis("Horizontal"); // Get the horizontal input from the user
        transform.position = transform.position + (Vector3.right) * horizontalInput * moveSpeed * Time.deltaTime; // Move the player to the left or right based on the horizontal input and moveSpeed variable
    }
}