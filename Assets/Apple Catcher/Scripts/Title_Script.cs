using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Script : MonoBehaviour
{
    public SpriteRenderer fader_renderer; // Public reference to a SpriteRenderer component
    public AudioClip leaveSound; // Public AudioClip variable to hold the sound played when leaving the title screen

    protected AudioSource r_audioSource; // Protected reference to an AudioSource component
    protected bool hasLeft = false; // Protected boolean variable to hold whether the player has left the title screen
    protected float current_alpha = 0; // Protected float variable to hold the current alpha value of the fader

    // Start is called before the first frame update
    void Start()
    {
        r_audioSource = GetComponent<AudioSource>(); // Get the AudioSource component and assign it to the r_audioSource variable
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) ) // Check if the escape key has been pressed
        {
            Application.Quit(); // Quit the application
        }
        else if ( Input.anyKeyDown && !hasLeft ) // Check if any key has been pressed and the player has not left yet
        {
            hasLeft = true; // Set the hasLeft variable to true
            StartCoroutine( LoadScene_Game() ); // Start the LoadScene_Game coroutine
        }
    }

    IEnumerator LoadScene_Game()
    {
        //Stop music and play the exit sound
        r_audioSource.Stop(); // Stop the AudioSource component
        r_audioSource.clip = leaveSound; // Set the clip of the AudioSource component to the leaveSound variable
        r_audioSource.loop = false; // Set the loop property of the AudioSource component to false
        r_audioSource.Play(); // Play the AudioSource component

        //Wait for that sound to end (with a margin)
        yield return new WaitForSeconds(0.8f); // Wait for 0.8 seconds

        //Fade the white fader into "existence"
        while ( current_alpha < 1) // Check if the current alpha value is less than 1
        {
            current_alpha += Time.deltaTime / 2; // Increase the current alpha value based on the time since the last frame
            fader_renderer.color = new Color(1, 1, 1, current_alpha); // Set the color of the fader based on the current alpha value
            yield return null; // Wait for the next frame
        }

        //Wait a tiny bit
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        //Load game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("AppleCatcher"); // Load the "AppleCatcher" scene asynchronously

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) // Check if the asynchronous scene is not done loading
        {
            yield return null; // Wait for the next frame
        }
    }
}