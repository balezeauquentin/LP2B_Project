using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    // Maximum velocity of the ball
    public float maxVelocity = 10f;
    // Delay before the ball respawns
    public float respawnDelay = 2f;
    // Reference to the score text
    public TextMeshPro displayed_score;
    // Current score
    protected int score = 0;
    // Reference to the ball's rigidbody
    public Rigidbody2D myRigidbody;
    // Flag to indicate if the ball is destroyed
    public static bool isDestroyed = true;
    // Reference to the paddle sound effect
    protected AudioSource ref_AudioSourcePaddle;
    public AudioClip paddleSound;
    // Reference to the wall sound effect
    protected AudioSource ref_AudioSourceWall;
    public AudioClip wallSound;
    // Reference to the lose life sound effect
    protected AudioSource ref_AudioSourceLooseLife;
    public AudioClip looseLifeSound;
    // Flag to indicate if the ball is stopped
    private bool isStopped = true;
    // Time when the ball was stopped
    private float stopTime = 0f;
    // Reference to the LivesScript component
    public LivesScript ref_livesScript;

    // Start is called before the first frame update
    void Start()
    {
        // Add audio sources for the sound effects
        ref_AudioSourceWall = gameObject.AddComponent<AudioSource>();
        ref_AudioSourceWall.volume = 0.5f;
        ref_AudioSourceWall.clip = wallSound;
        ref_AudioSourcePaddle = gameObject.AddComponent<AudioSource>();
        ref_AudioSourcePaddle.volume = 0.5f;
        ref_AudioSourcePaddle.clip = paddleSound;
        ref_AudioSourceLooseLife = gameObject.AddComponent<AudioSource>();
        ref_AudioSourceLooseLife.volume = 0.5f;
        ref_AudioSourceLooseLife.clip = looseLifeSound;

        // Find the LivesScript component in the scene
        ref_livesScript = FindObjectOfType<LivesScript>();
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(myRigidbody.velocity.y) < 1){

            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 1.1f);
        }
        // Limit the velocity of the ball
        if (myRigidbody.velocity.magnitude > maxVelocity)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxVelocity;
        }

        // Check if the ball is stopped and if it's time to move it
        if (isStopped && Time.time - stopTime >= respawnDelay)
        {
            // Choose a random angle and apply a force to the ball
            float angle = Random.Range(-30f, 30f);
            float respawnForce = 50f;
            Vector2 velocity = Quaternion.Euler(0f, 0f, angle) * Vector2.up * respawnForce;
            myRigidbody.velocity = velocity;
            isStopped = false;
        }
    }

    void Update()
    {
        // Check if the ball has fallen below the screen or if all bricks have been destroyed
        if (transform.position.y < -7f)
        {
            // Lose a life and reset the ball
            ref_livesScript.LoseLife();
            transform.position = new Vector2(0f, -5.5f);
            myRigidbody.velocity = Vector2.zero;
            isStopped = true;
            stopTime = Time.time;
        }
        else if (BrickSpawner.numBricks == 0)
        {
            transform.position = new Vector2(0f, -5.5f);
            myRigidbody.velocity = Vector2.zero;
            isStopped = true;
            stopTime = Time.time;
        }   
    }

    // Called when the ball collides with another object
    void OnCollisionEnter2D(Collision2D other)
    {
        float diffX = transform.position.x - other.transform.position.x;
        if (other.gameObject.tag == "Paddle")
        {
            // Bounce the ball off the paddle and play the paddle sound effect
            myRigidbody.velocity += new Vector2(diffX * 3, 0);
            ref_AudioSourcePaddle.Play();
        }
        else if (other.gameObject.CompareTag("Brick"))
        {
            // Destroy the brick, increase the score, and play the wall sound effect
            score += 50;
            displayed_score.SetText("Score : " + score);
            ref_AudioSourceWall.Play();
            Destroy(other.gameObject);
            BrickSpawner.numBricks--;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            // Play the wall sound effect
            ref_AudioSourceWall.Play();
        }
    }
}