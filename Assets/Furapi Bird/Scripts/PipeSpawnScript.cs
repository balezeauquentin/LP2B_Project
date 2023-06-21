using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe; // Public reference to a GameObject for the pipe
    public float spawnRate = 2; // Public float variable to hold the spawn rate of the pipes
    private float timer = 0; // Private float variable to hold the timer for spawning pipes
    public float heightOffset = 10; // Public float variable to hold the height offset for spawning pipes

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe(); // Call the spawnPipe method to spawn the first pipe
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate) { // Check if the timer is less than the spawn rate
            timer = timer + Time.deltaTime; // Increment the timer by the time since the last frame
        } else {
            spawnPipe(); // Call the spawnPipe method to spawn a new pipe
            timer = 0; // Reset the timer
        }
    }

    // Spawns a new pipe
    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset; // Calculate the lowest point for spawning the pipe
        float highestPoint = transform.position.y + heightOffset; // Calculate the highest point for spawning the pipe
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation); // Instantiate the pipe at a random height between the lowest and highest points
    }
}