using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Access Map.cs from LevelManager.cs

public class LevelManager : MonoBehaviour
{

    public GameObject player;
    public GameObject well;

    void Start()
    {
        InstatiatePlayer();
    }

    private void InstatiatePlayer()
    {
        GameObject spawnPoint = GameObject.Find("(0, 0)");
        Instantiate(well, spawnPoint.transform.position, Quaternion.identity);
        Instantiate(player, spawnPoint.transform.position, Quaternion.identity);
    }
}
