using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseAppleCatcher : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pauseMenu;
    protected bool isPause = false;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                isPause = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }else{
                //resume game
                pauseMenu.SetActive(false);
                Time.timeScale = 1; 
                isPause = false;
                }
        }
    }
    public void Restart()
    {
        StartCoroutine(LoadScene_Game("AppleCatcher"));
        Time.timeScale = 1;
    }

    public void MainMenu(){
        StartCoroutine(LoadScene_Game("Menu"));
        Time.timeScale = 1;
    }
    public void Quit(){
        Application.Quit();
    }
    IEnumerator LoadScene_Game(string sceneName)
    {
        //Load game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
