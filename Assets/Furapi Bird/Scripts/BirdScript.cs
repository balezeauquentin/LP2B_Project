// This is a comment
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody; // Rigidbody component of the bird
    public float flapStrength; // The strength of the bird's flap
    public LogicScript logic ; // Reference to the game logic script
    public bool birdIsAlive = true; // Flag to check if the bird is alive

    public Animator animator; // Animator component of the bird
    public AudioClip deathSoundClip; // Sound clip to play when the bird dies
    public AudioClip impactSoundClip; // Sound clip to play when the bird hits an obstacle
    protected AudioSource deathSound; // Audio source to play the death sound
    protected AudioSource impactSound; // Audio source to play the impact sound

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>(); // Find the game logic script
        animator.SetBool("Dead",false); // Set the "Dead" parameter of the animator to false
        deathSound = gameObject.AddComponent<AudioSource>(); // Add an audio source component to the bird
        deathSound.volume = 0.5f; // Set the volume of the death sound
        deathSound.clip = deathSoundClip; // Set the death sound clip
        impactSound = gameObject.AddComponent<AudioSource>(); // Add an audio source component to the bird
        deathSound.volume = 0.5f; // Set the volume of the impact sound
        deathSound.clip = deathSoundClip; // Set the impact sound clip
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Jump",myRigidbody.velocity.y); // Set the "Jump" parameter of the animator to the bird's vertical velocity

        if (transform.position.y < -30f) // If the bird falls below a certain height
        {
            myRigidbody.isKinematic = true; // Set the bird's rigidbody to kinematic
            myRigidbody.velocity = new Vector2(0,0); // Set the bird's velocity to zero
        }
        else
        {
            myRigidbody.isKinematic = false; // Set the bird's rigidbody to non-kinematic
        }

        if (transform.position.y > 17 || transform.position.y < -17) // If the bird goes out of bounds
        {
            deathSound.Play(); // Play the death sound
            animator.SetBool("Dead",true); // Set the "Dead" parameter of the animator to true
            logic.gameOver(); // Call the game over function in the game logic script
            birdIsAlive = false; // Set the birdIsAlive flag to false
        }

        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true) // If the space key is pressed and the bird is alive
        {
            myRigidbody.velocity = Vector2.up * flapStrength; // Set the bird's velocity to the flap strength vector
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  
        impactSound.Play(); // Play the impact sound
        deathSound.Play(); // Play the death sound

        animator.SetBool("Dead",true); // Set the "Dead" parameter of the animator to true
        logic.gameOver(); // Call the game over function in the game logic script
        birdIsAlive = false; // Set the birdIsAlive flag to false
    }
}