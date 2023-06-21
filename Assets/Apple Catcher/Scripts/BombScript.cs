using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public static bool bombIsDestroyed = true; // Public static boolean variable to hold whether the bomb has been destroyed
    public InternHitboxScript ref_internHitboxScript; // Public reference to an InternHitboxScript object

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if (transform.position.y < -5.5f) // Check if the bomb has fallen below a certain position
        {
            Destroy(gameObject); // Destroy the bomb object
            bombIsDestroyed = true; // Set the bombIsDestroyed variable to true
        }
    }

}