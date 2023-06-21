using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public AudioClip ref_audioClip; // Public AudioClip variable to hold the audio clip played when the game starts
    public SpriteRenderer fader_renderer; // Public reference to a SpriteRenderer component
    protected GameObject apple_prefab; // Protected reference to an Apple_prefab object
    protected GameObject bomb_prefab; // Protected reference to a Bomb_prefab object
    protected float timer = 3f; // Protected float variable to hold the timer value
    protected AudioSource ref_audioSource; // Protected reference to an AudioSource component
    protected float current_alpha = 1; // Protected float variable to hold the current alpha value of the fader

    // Start is called before the first frame update
    void Start()
    {   
        ref_audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to the game object and assign it to the ref_audioSource variable
        ref_audioSource.loop = true; // Set the loop property of the AudioSource component to true
        ref_audioSource.volume = 0.5f; // Set the volume property of the AudioSource component to 0.5
        ref_audioSource.clip = ref_audioClip; // Set the clip of the AudioSource component to the ref_audioClip variable

        StartCoroutine(FadeOutFromWhite()); // Start the FadeOutFromWhite coroutine
    }

    void Awake()
    {
        apple_prefab = Resources.Load<GameObject>("Apple_prefab"); // Load the Apple_prefab object and assign it to the apple_prefab variable
        bomb_prefab = Resources.Load<GameObject>("Bomb_prefab"); // Load the Bomb_prefab object and assign it to the bomb_prefab variable
    }

    // Update is called once per frame
    void Update()
    {
        if (AppleScript.appleIsDestroyed == true && BombScript.bombIsDestroyed == true) // Check if both the apple and bomb objects have been destroyed
        {
            float randomX = Random.value * 17f - 8.5f; // Generate a random X position for the new object
            float bombOrApple = Random.Range(0,3); // Generate a random number between 0 and 2
            GameObject newObject = Instantiate(bombOrApple < 1 ? bomb_prefab : apple_prefab); // Instantiate a new object based on the random number
            newObject.transform.position = new Vector3(randomX, 6.0f, 0); // Set the position of the new object
            if(bombOrApple < 1){
                BombScript.bombIsDestroyed = false; // Set the bombIsDestroyed variable to false
            }else{
                AppleScript.appleIsDestroyed = false; // Set the appleIsDestroyed variable to false
            }
        }
    }

    //Coroutine to fade out from white/launch music with a delay
    IEnumerator FadeOutFromWhite()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds

        ref_audioSource.Play(); // Play the AudioSource component

        while (current_alpha > 0) // Check if the current alpha value is greater than 0
        {
            current_alpha -= Time.deltaTime / 2; // Decrease the current alpha value based on the time since the last frame
            fader_renderer.color = new Color(1, 1, 1, current_alpha); // Set the color of the fader based on the current alpha value
            yield return null; // Wait for the next frame
        }

        Destroy(fader_renderer.gameObject); // Destroy the fader object
    }
}