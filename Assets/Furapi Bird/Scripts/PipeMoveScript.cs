using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

    public float moveSpeed = 10; // Public float variable to hold the speed at which the pipe moves
    public float deadZone = -30; // Public float variable to hold the position at which the pipe is destroyed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left)*moveSpeed * Time.deltaTime; // Move the pipe to the left based on the moveSpeed variable

        if (transform.position.x < deadZone) // Check if the pipe has moved past the deadZone position
        {
            Debug.Log("Pipe Deleted."); // Log a message to the console
            Destroy(gameObject); // Destroy the pipe object
        }
    }
}