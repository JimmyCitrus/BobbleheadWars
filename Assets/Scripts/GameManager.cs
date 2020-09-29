using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] spawnPoints;
    public GameObject alien;

    public int maxAliensOnScreen;
    public int totalAliens;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;

    private int aliensOnScreen = 0;
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;

        //This is our spawn time randomizer
        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;
        }
        generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        //This is the logic that determines if an alien should spawn
        if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
        {
            //Keeps track of previous alien spawns so aliens don't spawn at the same time in the same place
            List<int> previousSpawnLocations = new List<int>();

            //Limiting the amount of aliens by the number of spawn points
            if (aliensPerSpawn > spawnPoints.Length)
            {
                aliensPerSpawn = spawnPoints.Length - 1;
            }

            //spawning will never exceed the max amount configured
            aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn - totalAliens : aliensPerSpawn;

            for (int i = 0; i < aliensPerSpawn; i++)
            {
                //Increment if the amount is less than the total aliens on screen
                if (aliensOnScreen < maxAliensOnScreen)
                {
                    aliensOnScreen += 1;
                }
                //1 -1 indicates a spawn has not been picked yet
                int spawnPoint = -1;
                //2 Loop runs until it finds a spawn that is not -1
                while (spawnPoint == -1)
                {
                    //3 Looks for a random number as a spawn point
                    int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                    //4 Checks to see if location was a previous spawn point already
                    if (!previousSpawnLocations.Contains(randomNumber))
                    {
                        previousSpawnLocations.Add(randomNumber);
                        spawnPoint = randomNumber;
                    }
                }

                //Grabs a spawn based on the index generated previously
                GameObject spawnLocation = spawnPoints[spawnPoint];

                //Creates an instance of the alien prefab
                GameObject newAlien = Instantiate(alien) as GameObject;

                //Positions the alien at the spawn point
                newAlien.transform.position = spawnLocation.transform.position;

                //Gets a reference to the alien script
                Alien alienScript = newAlien.GetComponent<Alien>();

                //Sets target to marine's current position
                alienScript.target = player.transform;

                //Rotate the alien towards the marine
                Vector3 targetRotation = new Vector3(player.transform.position.x, newAlien.transform.position.y, player.transform.position.z);
                newAlien.transform.LookAt(targetRotation);
            }
        }

        
       
    }
}
