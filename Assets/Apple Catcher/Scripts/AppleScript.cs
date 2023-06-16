using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public static bool appleIsDestroyed = true;
    public InternHitboxScript ref_internHitboxScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
            appleIsDestroyed = true;
            InternHitboxScript.score -= 5;
        }
    }
}
