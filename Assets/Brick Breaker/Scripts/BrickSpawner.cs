using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickSpawner : MonoBehaviour
{
    // Reference to the brick prefab
    public GameObject brickPrefab;
    // Array of colors for the bricks
    public Color[] brickColors = new Color[5];
    // Number of bricks in the scene
    public static int numBricks = 0; // Public static integer variable to hold the number of bricks in the scene
    // Flag to indicate if new bricks need to be created
    public static bool createBricks = false; // Public static boolean variable to indicate if new bricks need to be created
    // Reference to the intro sound effect
    protected AudioSource ref_AudioSourceIntro;
    public AudioClip introSound;

    // Start is called before the first frame update
    void Start()
    {
        // Load the brick prefab from the resources folder
        brickPrefab = Resources.Load<GameObject>("BrickG_prefab");

        // Add an audio source for the intro sound effect and play it
        ref_AudioSourceIntro = gameObject.AddComponent<AudioSource>();
        ref_AudioSourceIntro.volume = 0.5f;
        ref_AudioSourceIntro.clip = introSound;
        ref_AudioSourceIntro.Play();

        // Initialize the colors for the bricks
        InitializeColors();
        // Generate the bricks in the scene
        GenerateBricks();
    }

    void Awake()
    {
        // Load the brick prefab from the resources folder
        brickPrefab = Resources.Load<GameObject>("BrickG_prefab");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all bricks have been destroyed
        if (numBricks == 0)
        {
            // Set the flag to create new bricks and generate them
            createBricks = true;
            GenerateBricks();
        }
    }

    // Generates the bricks in the scene
    public void GenerateBricks()
    {
        GameObject newBrick = null;

        const int MAX_BRICKS = 40; // Constant integer variable to hold the maximum number of bricks
        int MAX_CULUMN = 11; // Integer variable to hold the maximum number of columns
        int MAX_ROW = 5; // Integer variable to hold the maximum number of rows
        int culumnNumber = 0; // Integer variable to hold the current column number
        int rowNumber = 0; // Integer variable to hold the current row number
        float diffX = 1.425f; // Float variable to hold the difference in X position between bricks
        float diffY = 0.75f; // Float variable to hold the difference in Y position between bricks
        for (rowNumber = 0; rowNumber <= MAX_ROW; rowNumber++)
        {
            for (culumnNumber = 0; culumnNumber <= MAX_CULUMN; culumnNumber++)
            {
                // Choose a random number and color for the brick
                float randomBrick = Random.Range(0f, 2f);
                int randomColorBrick = Random.Range(0, 4);
                if (randomBrick > 0.7f && numBricks < MAX_BRICKS)
                {
                    // Instantiate the brick, set its color, position, and increment the number of bricks
                    newBrick = Instantiate(brickPrefab);
                    SpriteRenderer brickRenderer = newBrick.GetComponent<SpriteRenderer>();
                    brickRenderer.color = brickColors[randomColorBrick];
                    newBrick.transform.position = new Vector2(-7.83f + diffX * culumnNumber, 3f - diffY * rowNumber);
                    numBricks++;
                }

            }
            culumnNumber = 0;
        }
        // Reset the flag to create new bricks
        createBricks = false;
    }

    // Initializes the colors for the bricks
    private void InitializeColors()
    {
        brickColors[0] = new Color(0.4901961f, 0.5529412f, 0.972549f);
        brickColors[1] = new Color(0.945098f, 0.4156863f, 0.4156863f);
        brickColors[2] = new Color(0.8962264f, 0.879f, 0.2747864f);
        brickColors[3] = new Color(0.8773585f, 0.2772784f, 0.8251776f);
    }
}