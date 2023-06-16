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
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            {
            newSpeedX = 0;
            }
        else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(transform.position.x > -7.332f)
                {
                    newSpeedX = -15f;
                }
            }
        else if ( Input.GetKey(KeyCode.RightArrow))
            {
                if(transform.position.x < 7.32f){
                    newSpeedX = 15f;
                }
            }
        transform.Translate(newSpeedX * Time.deltaTime, 0, 0);
    }
}
