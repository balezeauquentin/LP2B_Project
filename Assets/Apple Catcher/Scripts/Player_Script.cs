using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Script : MonoBehaviour
{

    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    protected AudioSource ref_audioSource; // Protected reference to an AudioSource component
    protected Animator ref_animator; // Protected reference to an Animator component

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        ref_audioSource = GetComponent<AudioSource>(); // Get the AudioSource component and assign it to the ref_audioSource variable
        ref_animator = GetComponent<Animator>(); // Get the Animator component and assign it to the ref_animator variable
    }

    // Update is called once per frame
    void Update()
    {

        //Manage movement speed and animations
        float newSpeed = 0; // Initialize a float variable to hold the new speed
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) // Check if both the left and right arrow keys are being pressed
        {
            newSpeed = 0; // Set the new speed to 0
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) // Check if the left arrow key is being pressed
        {
            if (transform.position.x > -8.2f) // Check if the player is not at the left edge of the screen
            {
                newSpeed = -12f; // Set the new speed to move the player to the left
                ref_animator.SetBool("isForwards", false); // Set the "isForwards" parameter in the animator to false
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // Check if the right arrow key is being pressed
        {
            if (transform.position.x < 7.85f) // Check if the player is not at the right edge of the screen
            {
                newSpeed = 12f; // Set the new speed to move the player to the right
                ref_animator.SetBool("isForwards", true); // Set the "isForwards" parameter in the animator to true
            }
        }

        //Inform animator : Are we moving?
        ref_animator.SetBool("isMoving", newSpeed != 0); // Set the "isMoving" parameter in the animator based on whether the new speed is not 0

        //Move with the speed found
        transform.Translate(newSpeed * Time.deltaTime, 0, 0); // Move the player based on the new speed
    }
}