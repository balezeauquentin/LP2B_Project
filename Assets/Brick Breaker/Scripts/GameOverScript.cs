using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
