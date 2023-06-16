using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject brickPrefab;
    public Color[] brickColors = new Color[5];
    protected GameObject brickGreen_prefab;
    public static int numBricks = 0;
    public static bool createBricks = false;
    protected AudioSource ref_AudioSourceIntro;
    public AudioClip introSound;

    // Start is called before the first frame update
    void Start()
    {
        brickPrefab = Resources.Load<GameObject>("BrickG_prefab");

        ref_AudioSourceIntro = gameObject.AddComponent<AudioSource>();
        ref_AudioSourceIntro.volume = 0.5f;
        ref_AudioSourceIntro.clip = introSound;
        ref_AudioSourceIntro.Play();

        InitializeColors();
        GenerateBricks();


    }

    // Update is called once per frame
    void Update()
    {
        if (createBricks == true)
        {
            GenerateBricks();
            createBricks = false;
        }
        else if (numBricks == 0)
        {
            GenerateBricks();
        }
    }
    public void GenerateBricks()
    {
        GameObject newBrick = null;
        const int MAX_BRICKS = 40;
        int MAX_CULUMN = 11;
        int MAX_ROW = 5;
        int culumnNumber = 0;
        int rowNumber = 0;
        float diffX = 1.425f;
        float diffY = 0.75f;
        for (rowNumber = 0; rowNumber <= MAX_ROW; rowNumber++)
        {
            for (culumnNumber = 0; culumnNumber <= MAX_CULUMN; culumnNumber++)
            {
                float randomBrick = Random.Range(0f, 2f);
                int randomColorBrick = Random.Range(0, 4);
                if (randomBrick > 0.7f && numBricks < MAX_BRICKS)
                {
                    newBrick = Instantiate(brickPrefab);
                    SpriteRenderer brickRenderer = newBrick.GetComponent<SpriteRenderer>();
                    brickRenderer.color = brickColors[randomColorBrick];
                    newBrick.transform.position = new Vector2(-7.83f + diffX * culumnNumber, 3f - diffY * rowNumber);
                    numBricks++;
                }

            }
            culumnNumber = 0;
        }
        createBricks = false;
    }

    private void InitializeColors()
    {
        brickColors[0] = new Color(0.4901961f, 0.5529412f, 0.972549f);
        brickColors[1] = new Color(0.945098f, 0.4156863f, 0.4156863f);
        brickColors[2] = new Color(0.8962264f, 0.879f, 0.2747864f);
        brickColors[3] = new Color(0.8773585f, 0.2772784f, 0.8251776f);
    }
}


