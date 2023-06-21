using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseAppleCatcher : MonoBehaviour
{
    // Reference to the pause menu GameObject
    public GameObject pauseMenu;
    public TextMeshPro displaytext; // Reference to the TextMeshPro component for displaying text

    // Boolean to keep track of whether the game is paused or not
    protected bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set the time scale to 1 and deactivate the pause menu
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // If the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is not paused, pause the game and activate the pause menu
            if (!isPause)
            {
                displaytext.text = "Pause"; // Set the text to "Pause"
                displaytext.rectTransform.localPosition = new Vector2(185, 130); // Set the position of the text
                isPause = true; // Set isPause to true
                Time.timeScale = 0; // Set the time scale to 0 to pause the game
                pauseMenu.SetActive(true); // Activate the pause menu
            }
            // If the game is paused, resume the game and deactivate the pause menu
            else
            {
                isPause = false; // Set isPause to false
                pauseMenu.SetActive(false); // Deactivate the pause menu
                Time.timeScale = 1; // Set the time scale to 1 to resume the game
            }
        }
        // If the player has no lives left
        if (InternHitboxScript.lives == 0)
        {
            displaytext.text = "Game Over"; // Set the text to "Game Over"
            displaytext.rectTransform.localPosition = new Vector2(140, 130); // Set the position of the text
            isPause = true; // Set isPause to true
            Time.timeScale = 0; // Set the time scale to 0 to pause the game
            pauseMenu.SetActive(true); // Activate the pause menu
        }
    }

    // Restarts the game
    public void Restart()
    {
        // Reload the current scene asynchronously
        StartCoroutine(LoadScene_Game("AppleCatcher"));

        // Resume the game
        StartCoroutine(ResetGame());
    }

    // Returns to the main menu
    public void MainMenu()
    {
        // Load the main menu scene asynchronously
        StartCoroutine(LoadScene_Game("Menu"));
        StartCoroutine(ResetGame());
    }

    // Quits the game
    public void Quit()
    {
        Application.Quit(); // Quit the game
    }

    // Loads a scene asynchronously
    IEnumerator LoadScene_Game(string sceneName)
    {
        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    // Resets the game
    IEnumerator ResetGame()
    {
        yield return new WaitForEndOfFrame();

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
}