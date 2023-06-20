using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    public float maxVelocity = 10f; // Maximum velocity of the ball
    public float respawnDelay = 2f; // Delay before the ball respawns
    public TextMeshPro displayed_score;
    protected int score = 0;
    public Rigidbody2D myRigidbody;
    public static bool isDestroyed = true;
    protected AudioSource ref_AudioSourcePaddle;
    public AudioClip paddleSound;
    protected AudioSource ref_AudioSourceWall;
    public AudioClip wallSound;
    protected AudioSource ref_AudioSourceLooseLife;
    public AudioClip looseLifeSound;
    private bool isStopped = true; // Flag to indicate if the ball is stopped
    private float stopTime = 0f; // Time when the ball was stopped
    // Start is called before the first frame update
    public LivesScript ref_livesScript;
    void Start()
    {
        ref_AudioSourceWall = gameObject.AddComponent<AudioSource>();
        ref_AudioSourceWall.volume = 0.5f;
        ref_AudioSourceWall.clip = wallSound;
        ref_AudioSourcePaddle = gameObject.AddComponent<AudioSource>();
        ref_AudioSourcePaddle.volume = 0.5f;
        ref_AudioSourcePaddle.clip = paddleSound;
        ref_AudioSourceLooseLife = gameObject.AddComponent<AudioSource>();
        ref_AudioSourceLooseLife.volume = 0.5f;
        ref_AudioSourceLooseLife.clip = looseLifeSound;
        ref_livesScript = FindObjectOfType<LivesScript>();
    }


    void FixedUpdate()
    {
        // Limit the velocity of the ball


        // Check if the ball is stopped and if it's time to move it
        if (isStopped && Time.time - stopTime >= respawnDelay)
        {
            float angle = Random.Range(-30f, 30f);
            float respawnForce = 50f;
            Vector2 velocity = Quaternion.Euler(0f, 0f, angle) * Vector2.up * respawnForce;
            myRigidbody.velocity = velocity;
            isStopped = false;
        }
    }

    void Update()
    {
        if (myRigidbody.velocity.magnitude > maxVelocity)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxVelocity;
        }
        if (transform.position.y < -7f || BrickSpawner.createBricks == true)
        {
            ref_livesScript.LoseLife();
            transform.position = new Vector2(0f, -5.5f);
            myRigidbody.velocity = Vector2.zero;
            isStopped = true;
            stopTime = Time.time;
        }

    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        float diffX = transform.position.x - other.transform.position.x;
        if (other.gameObject.tag == "Paddle")
        {
            myRigidbody.velocity += new Vector2(diffX * 3, 0);
            ref_AudioSourcePaddle.Play();
        }
        else if (other.gameObject.CompareTag("Brick"))
        {
            score += 50;
            displayed_score.SetText("Score : " + score);
            ref_AudioSourceWall.Play();
            Destroy(other.gameObject);
            BrickSpawner.numBricks--;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            ref_AudioSourceWall.Play();
        }
    }
}
