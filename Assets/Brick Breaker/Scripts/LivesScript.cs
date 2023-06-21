using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesScript : MonoBehaviour
{
    // The number of lives the player has
    public static int lives = 5;
    // The text object to display the number of lives
    public TextMeshPro display_menu_text;
    // The prefab for the ball object
    protected GameObject ball_prefab;
    // A list of ball objects in the scene
    private List<GameObject> ballObjects = new List<GameObject>();
    // The game over menu object
    public GameObject GameOverMenu;
    // The current number of lives
    protected int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate the game over menu
        GameOverMenu.SetActive(false);
        // Load the ball prefab from the resources folder
        ball_prefab = Resources.Load<GameObject>("Ball_prefab");
        // Set the current number of lives to the initial number of lives
        currentLives = lives;
        // Spawn the balls in the scene
        SpawnBalls();
    }

    // Spawns the balls in the scene
    void SpawnBalls()
    {
        for (int i = 0; i < currentLives; i++)
        {
            // Instantiate a new ball object and add it to the list of ball objects
            GameObject newObject = Instantiate(ball_prefab);
            ballObjects.Add(newObject);
            // Position the ball object based on the number of lives remaining
            if (i < 3)
            {
                newObject.transform.position = new Vector3(4.9f + i * 1.2f, 6.1f, 0);
            }
            else
            {
                newObject.transform.position = new Vector3(5.5f + (i-3) * 1.2f, 5.1f, 0);
            }
        }
    }

    // Called when the player loses a life
    public void LoseLife()
    {
        // Decrement the current number of lives
        currentLives--;
        // If there are still lives remaining, destroy the ball object
        if (currentLives >= 0)
        {
            Destroy(ballObjects[currentLives]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the player has no lives remaining, display the game over menu and pause the game
        if(currentLives == 0)
        {
            display_menu_text.text = "Game Over";
            display_menu_text.rectTransform.localPosition = new Vector2 (40f, 60f);
            GameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}