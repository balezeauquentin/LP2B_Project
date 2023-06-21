using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    AudioSource clickSound;
    public AudioClip clickClip;

    // Start is called before the first frame update
    void Start()
    {
        // Add an audio source for the click sound effect and set its volume and clip
        clickSound = gameObject.AddComponent<AudioSource>();
        clickSound.volume = 0.3f;
        clickSound.clip = clickClip;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the escape key is pressed and quit the application if it is
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Loads the Apple Catcher game scene
    public void LoadAppleCatcher()
    {
        StartCoroutine(LoadScene_Game("TitleAppleCatcher"));

    }

    // Loads the Brick Breaker game scene
    public void LoadBrickBreaker()
    {
        StartCoroutine(LoadScene_Game("BrickBreaker"));

    }

    // Loads the Furapi Bird game scene
    public void LoadFurapiBird()
    {
        StartCoroutine(LoadScene_Game("FurapiBird"));
    }

    // Coroutine to load a game scene
    IEnumerator LoadScene_Game(string sceneName)
    {
        // Play the click sound effect and wait for it to finish
        clickSound.Play();
        yield return new WaitForSeconds(clickClip.length);

        // Load the game scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // Coroutine to reset the Apple Catcher game
    IEnumerator ResetAppleCatcher()
    {
        yield return new WaitForEndOfFrame();

        // Wait until the Apple Catcher scene is fully loaded
        while (!SceneManager.GetSceneByName("AppleCatcher").isLoaded)
        {
            yield return null;
        }

        // Wait for an additional frame to ensure that all objects are initialized
        yield return new WaitForEndOfFrame();

        // The scene is now fully loaded, continue with your code here
        AppleScript.appleIsDestroyed = true; // Set appleIsDestroyed to true
        BombScript.bombIsDestroyed = true; // Set bombIsDestroyed to true
        InternHitboxScript.score = 0; // Reset the score to 0
        InternHitboxScript.lives = 3; // Reset the lives to 3

        Time.timeScale = 1; // Resume the game
    }

    // Coroutine to reset the Brick Breaker game
    IEnumerator ResetBrickBreaker()
    {
        yield return new WaitForEndOfFrame();

        // Wait until the Brick Breaker scene is fully loaded
        while (!SceneManager.GetSceneByName("BrickBreaker").isLoaded)
        {
            yield return null;
        }

        // Wait for an additional frame to ensure that all objects are initialized
        yield return new WaitForEndOfFrame();

        // The scene is now fully loaded, continue with your code here
        BrickSpawner.numBricks = 0; // Reset the number of bricks to 0
        Time.timeScale = 1; // Resume the game
    }
}