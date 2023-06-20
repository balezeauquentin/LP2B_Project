using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesScript : MonoBehaviour
{
    public static int lives = 5;
    public TextMeshPro display_menu_text;
    protected GameObject ball_prefab;
    private List<GameObject> ballObjects = new List<GameObject>();
    public GameObject GameOverMenu;
    protected int currentLives;
    // Start is called before the first frame update
    void Start()
    {
        GameOverMenu.SetActive(false);
        ball_prefab = Resources.Load<GameObject>("Ball_prefab");
        currentLives = lives;
        SpawnBalls();
    }

    void SpawnBalls()
    {
        for (int i = 0; i < currentLives; i++)
        {
            GameObject newObject = Instantiate(ball_prefab);
            ballObjects.Add(newObject);
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

    public void LoseLife()
    {
        currentLives--;
        if (currentLives >= 0)
        {
            Destroy(ballObjects[currentLives]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(currentLives == 0)
        {
            display_menu_text.SetText("Game Over");
            display_menu_text.rectTransform.localPosition = new Vector2 (131.2021f, 60f);
            GameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
}}
