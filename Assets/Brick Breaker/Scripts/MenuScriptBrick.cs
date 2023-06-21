using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScriptBrick : MonoBehaviour
{
    // Reference to the pause menu GameObject
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        // If the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the pause menu is not active, activate it and pause the game
            if (!pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            // If the pause menu is active, deactivate it and resume the game
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    // Reloads the current scene

    // Restarts the game
    public void Restart()
    {
        // Load the game scene asynchronously
        StartCoroutine(LoadScene_Game("BrickBreaker"));

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
        Application.Quit();
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

        // Wait for an additional frame to ensure that all objects are initialized
        yield return new WaitForEndOfFrame();
    }

    // Resets the game
    IEnumerator ResetGame()
    {
        yield return new WaitForEndOfFrame();

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