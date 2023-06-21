using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public static bool appleIsDestroyed = true; // Public static boolean variable to hold whether the apple has been destroyed
    public InternHitboxScript ref_internHitboxScript; // Public reference to an InternHitboxScript object

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.5f) // Check if the apple has fallen below a certain position
        {
            Destroy(gameObject); // Destroy the apple object
            appleIsDestroyed = true; // Set the appleIsDestroyed variable to true
            InternHitboxScript.score -= 5; // Subtract 5 from the score variable in the InternHitboxScript object
        }
    }
}