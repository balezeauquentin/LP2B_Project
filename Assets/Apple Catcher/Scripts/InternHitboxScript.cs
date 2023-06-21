using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InternHitboxScript : MonoBehaviour
{
    public AudioClip ref_audioClip; // Public AudioClip variable to hold the audio clip played when the player hits an object
    public TextMeshPro display_score_text; // Public reference to a TextMeshPro object to display the score
    public TextMeshPro display_lives_text; // Public reference to a TextMeshPro object to display the number of lives
    public static int lives = 3; // Public static integer variable to hold the number of lives
    public static int score = 0; // Public static integer variable to hold the score
    protected AudioSource ref_audioSource; // Protected reference to an AudioSource component

    // Start is called before the first frame update
    void Start()
    {       
        ref_audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to the game object and assign it to the ref_audioSource variable
        ref_audioSource.volume = 0.5f; // Set the volume property of the AudioSource component to 0.5
        ref_audioSource.clip = ref_audioClip; // Set the clip of the AudioSource component to the ref_audioClip variable
        lives = 3; // Set the lives variable to 3
        score = 0; // Set the score variable to 0
    }

    // Update is called once per frame
    void Update()
    {
        display_score_text.SetText("Score : " + score); // Set the text of the display_score_text object to the current score
        display_lives_text.SetText("Lives : " + lives); // Set the text of the display_lives_text object to the current number of lives
    }

    void OnCollisionEnter2D( Collision2D other)
    {  
        if(other.collider.CompareTag("Apple")) // Check if the object collided with is an apple
        {
            score += 10; // Add 10 to the score variable
            Destroy(other.gameObject); // Destroy the apple object
            ref_audioSource.Play(); // Play the AudioSource component
            AppleScript.appleIsDestroyed = true; // Set the appleIsDestroyed variable to true
        }
        else if(other.collider.CompareTag("Bomb")) // Check if the object collided with is a bomb
        {
            lives--; // Subtract 1 from the lives variable
            Destroy(other.gameObject); // Destroy the bomb object
            ref_audioSource.Play(); // Play the AudioSource component
            BombScript.bombIsDestroyed = true; // Set the bombIsDestroyed variable to true
        }
    }
}