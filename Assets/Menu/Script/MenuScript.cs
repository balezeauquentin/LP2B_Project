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
        clickSound = gameObject.AddComponent<AudioSource>();
        clickSound.volume = 0.3f;
        clickSound.clip = clickClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadAppleCatcher()
    {
        StartCoroutine(LoadScene_Game("TitleAppleCatcher"));
    }

    public void LoadBrickBreaker()
    {
        StartCoroutine(LoadScene_Game("BrickBreaker"));
    }

    public void LoadFurapiBird()
    {
        StartCoroutine(LoadScene_Game("FurapiBird"));
    }


    IEnumerator LoadScene_Game(string sceneName)
    {
        clickSound.Play();
        yield return new WaitForSeconds(clickClip.length);
        //Load game scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}