using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Obstacle_Spawn : MonoBehaviour
{
    public GameObject logPrefab;
    public GameObject boulderPrefab;
    public GameObject obstaclesFolder;

    public bool logSpawner;
    public bool boulderSpawner;
    private GameObject obstacleToSpawn;

    public float spawnRate;
    private float timer = 0;

    void Start()
    {

        if (logSpawner)
        {
            spawnRate = 6f; // 4f
            obstacleToSpawn = logPrefab;
        }

        if (boulderSpawner)
        {
            spawnRate = 5f; //3f
            obstacleToSpawn = boulderPrefab;
            spawnRate += Random.Range(1f, 3f);
        }

        spawnObstacle(obstacleToSpawn);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (logSpawner)
        {

            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) spawnRate = (29000 - Stats_Manager.Instance.distancesTravelled) / 4750;
        }
        if (boulderSpawner)
        {
            
            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) spawnRate = (24250 - Stats_Manager.Instance.distancesTravelled) / 4750;
        }

        //Checks if enough time has passed between obstacle spawns
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            

            if (obstacleToSpawn != null)
            {
                spawnObstacle(obstacleToSpawn);
            }
            //Debug.Log(spawnRate);
            timer = 0;
        }
    }

    void spawnObstacle(GameObject obstacle)
    {

        //Spawns an obstacle between the bounds of the left and right walls
        Instantiate(obstacle, new Vector3(Random.Range(Game_Manager.Instance.meleeLeftSpawnerBound, Game_Manager.Instance.meleeRightSpawnerBound), transform.position.y, transform.position.z), transform.rotation, obstaclesFolder.transform);
    }
}
