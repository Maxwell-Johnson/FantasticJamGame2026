using UnityEngine;
using UnityEngine.Rendering;

public class Obstacle_Spawn : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject obstaclesFolder;

    public float spawnRate = 1;
    public float maxSpawnRate = 3f;
    public float minSpawnRate = 0.1f;
    private float timer = 0;

    void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        spawnObstacle();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Checks if enough time has passed between obstacle spawns
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnObstacle();
            //Debug.Log(spawnRate);
            timer = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void spawnObstacle()
    {
        //Spawns an obstacle between the bounds of the left and right walls
        Instantiate(obstacle, new Vector3(Random.Range(Game_Manager.Instance.leftSpawnerBound, Game_Manager.Instance.rightSpawnerBound), transform.position.y, transform.position.z), transform.rotation, obstaclesFolder.transform);
    }
}
