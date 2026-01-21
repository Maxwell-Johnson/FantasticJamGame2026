using System.Collections.Generic;
using UnityEngine;

public class Powerup_Spawner : MonoBehaviour
{

    public GameObject powerupPrefab;
    public GameObject powerupsFolder;
    public float spawnRate;
    public float maxSpawnRate = 10f;
    public float minSpawnRate = 17f;
    private float timer = 0;
    private int maxSpawnAmount = 2;


    //Keeps track of all the spawns in a list
    public List<GameObject> powerupList = new List<GameObject>();

    private void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    private void FixedUpdate()
    {
        //Checks if enough time has passed between obstacle spawns
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPowerup();
            //Debug.Log(spawnRate);
            timer = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    private void SpawnPowerup()
    {
        if (powerupList.Count < maxSpawnAmount)
        {
            powerupList.Add(Instantiate(powerupPrefab, new Vector3(Random.Range(Game_Manager.Instance.meleeLeftSpawnerBound, Game_Manager.Instance.meleeRightSpawnerBound), transform.position.y, transform.position.z), transform.rotation, powerupsFolder.transform));
        }
    }

}
