using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InternHitboxScript : MonoBehaviour
{
    public AudioClip ref_audioClip;
    public TextMeshPro display_score_text;
    public TextMeshPro display_lives_text;
    public static int lives = 3;
    public static int score = 0;
    protected AudioSource ref_audioSource;  
    // Start is called before the first frame update
    void Start()
    {       
        ref_audioSource = gameObject.AddComponent<AudioSource>();
        ref_audioSource.volume = 0.5f;
        ref_audioSource.clip = ref_audioClip;

    }

    // Update is called once per frame
    void Update()
    {
        display_score_text.SetText("Score : " + score);
        display_lives_text.SetText("Lives : " + lives);
    }
    void OnCollisionEnter2D( Collision2D other)
    {  
        if(other.collider.CompareTag("Apple"))
        {
            score += 10;
            Destroy(other.gameObject);
            ref_audioSource.Play();
            AppleScript.appleIsDestroyed = true;
        }else if(other.collider.CompareTag("Bomb"))
        {

            //TODO: enlever une vie
            lives--;
            Destroy(other.gameObject);
            ref_audioSource.Play();
            BombScript.bombIsDestroyed = true;
        }

    }
}
