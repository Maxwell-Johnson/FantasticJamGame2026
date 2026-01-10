using UnityEngine;
using UnityEngine.Rendering;

public class Obstacle_Spawn : MonoBehaviour
{
    public GameObject obstacle;
    public float spawnRate = 2;
    private float timer = 0;

    //Boundries on left and right walls for spawning obstacles
    private float leftBound = -2.5f;
    private float rightBound = 4.5f;
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if enough time has passed between obstacle spawns
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }
    }

    void spawnPipe()
    {
        //Spawns an obstacle between the bounds of the left and right walls
        Instantiate(obstacle, new Vector3(Random.Range(leftBound, rightBound), transform.position.y, transform.position.z), transform.rotation);
    }
}
