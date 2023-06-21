using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newSpeedX = 0;
        // If both left and right arrow keys are pressed, don't move the paddle
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            newSpeedX = 0;
        }
        // If only the left arrow key is pressed, move the paddle left
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            // If the paddle is not at the left edge of the screen, move it left
            if(transform.position.x > -7.332f)
            {
                newSpeedX = -15f;
            }
        }
        // If only the right arrow key is pressed, move the paddle right
        else if ( Input.GetKey(KeyCode.RightArrow))
        {
            // If the paddle is not at the right edge of the screen, move it right
            if(transform.position.x < 7.32f)
            {
                newSpeedX = 15f;
            }
        }
        // Move the paddle based on the new speed
        transform.Translate(newSpeedX * Time.deltaTime, 0, 0);
    }
}
