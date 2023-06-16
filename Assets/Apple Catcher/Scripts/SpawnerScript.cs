using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public AudioClip ref_audioClip;
    public SpriteRenderer fader_renderer;
    protected GameObject apple_prefab;
    protected GameObject bomb_prefab;
    protected float timer = 3f;
    protected AudioSource ref_audioSource;
    protected float current_alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        apple_prefab = Resources.Load<GameObject>("Apple_prefab");
        bomb_prefab = Resources.Load<GameObject>("Bomb_prefab");

        ref_audioSource = gameObject.AddComponent<AudioSource>();
        ref_audioSource.loop = true;
        ref_audioSource.volume = 0.5f;
        ref_audioSource.clip = ref_audioClip;

        StartCoroutine(FadeOutFromWhite());

    }

    // Update is called once per frame
    void Update()
    {
        float lastTime = 0f;
            if (Time.time - lastTime >= 1f)
    {
        // La condition s'active toutes les secondes
        lastTime = Time.time;
        // Ajoutez votre code ici
        Debug.Log(" stat: " + BombScript.bombIsDestroyed);
    }
        if (AppleScript.appleIsDestroyed == true && BombScript.bombIsDestroyed == true)
        {
            float randomX = Random.value * 17f - 8.5f;
            float bombOrApple = Random.Range(0,3);
            GameObject newObject = Instantiate(bombOrApple < 1 ? bomb_prefab : apple_prefab);
            newObject.transform.position = new Vector3(randomX, 6.0f, 0);
            if(bombOrApple < 1){
                BombScript.bombIsDestroyed = false;
            }else{
                AppleScript.appleIsDestroyed = false;
            }
        }
        
    }

    //Coroutine to fade out from white/launch music with a delay
    IEnumerator FadeOutFromWhite()
    {
        yield return new WaitForSeconds(0.5f);

        ref_audioSource.Play();

        while (current_alpha > 0)
        {
            current_alpha -= Time.deltaTime / 2;
            fader_renderer.color = new Color(1, 1, 1, current_alpha);
            yield return null;
        }

        Destroy(fader_renderer.gameObject);

    }
}
