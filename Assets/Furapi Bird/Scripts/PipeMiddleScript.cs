using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{

    public LogicScript logic ; // Public reference to a LogicScript object
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>(); // Find the LogicScript object and assign it to the logic variable
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) // Called when a 2D collider enters the trigger
    {
        if (collision.gameObject.layer == 3) // Check if the collider is on the "Player" layer
        {
            logic.addScore(1); // Call the addScore method on the LogicScript object to increase the player's score
        }
    }
}