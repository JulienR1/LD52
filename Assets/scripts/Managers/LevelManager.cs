using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Access Map.cs from LevelManager.cs

public class LevelManager : MonoBehaviour
{

    public GameObject player;
    public GameObject well;
    public GameObject[] spawners;

    private int spawnerId = -1;

    GameObject levelZeroZero;

    void Start()
    {
        levelZeroZero = GameObject.Find("(0, 0)");
        
        InstatiatePlayer(levelZeroZero);
        InstantiateSpawners(levelZeroZero);
    }

    private void InstatiatePlayer(GameObject levelZeroZero)
    {
        Instantiate(well, levelZeroZero.transform.position, Quaternion.identity);
        Instantiate(player, levelZeroZero.transform.position, Quaternion.identity);
    }

    private void InstantiateSpawners(GameObject levelZeroZero){
        if(spawners.Length > 0)
        {
            foreach (GameObject spawner in spawners)
            {
                spawnerId++;
                GameObject newSpawner = Instantiate(spawner, levelZeroZero.transform.position, Quaternion.identity);
                newSpawner.name = "spawner_" + spawnerId;
                newSpawner.GetComponent<Spawner>().spawnerId = spawnerId;
            }
        }
    }
}
