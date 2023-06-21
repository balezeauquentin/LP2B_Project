using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore; // Public integer variable to hold the player's score
    public Text scoreText; // Public Text object to display the player's score
    public GameObject gameOverScreen; // Public GameObject to hold the game over screen

    [ContextMenu("Increase Score")] // Attribute to add a context menu item to increase the score
    public void addScore(int scoreToAdd) // Public method to add to the player's score
    {
        playerScore = playerScore + scoreToAdd; // Add the scoreToAdd parameter to the player's score
        scoreText.text = "Score : " + playerScore; // Update the scoreText object to display the new score
    }

    public void restartGame() // Public method to restart the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load the current scene to restart the game
    }

    public void loadMenu() // Public method to load the menu scene
    {
        StartCoroutine(LoadScene_Game("Menu")); // Start a coroutine to load the "Menu" scene
    }

    public void gameOver() // Public method to display the game over screen
    {
        gameOverScreen.SetActive(true); // Set the game over screen to active
    }

    IEnumerator LoadScene_Game(string sceneName) // Private coroutine to load a scene
    {
        //Load game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName); // Load the specified scene asynchronously

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait for the next frame
        }
    }
}