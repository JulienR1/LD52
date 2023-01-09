using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnerId;
    
    public GameObject[] animalList;

    public int animalCountStart;
    public int spawnerWidth = 4;
    public int spawnerHeight = 4;
    public int maxAnimalsInSpawner = 10;
    
    private float lastSpawn;
    public float spawnRateInSec = 5f;

    void Start()
    {
       
        for (int i = 0; i < animalCountStart; i++){
            SpawnAnimal();
        }
        lastSpawn = Time.time;
    }

    void Update()
    {
        if(Time.time - lastSpawn > spawnRateInSec){
            SpawnAnimal();
            lastSpawn = Time.time;
        }
    }    
    
    private void SpawnAnimal(){
        if(transform.childCount >= maxAnimalsInSpawner) return;
        
        GameObject animal = Instantiate(animalList[Random.Range(0, animalList.Length)], SpawnAt(), Quaternion.identity);
        animal.transform.parent = GameObject.Find("spawner_" + spawnerId).transform;
    } 

    private Vector3 SpawnAt(){
        GameObject spawnTile = GameObject.Find("(" + Random.Range(-spawnerWidth, spawnerHeight) + ", " + Random.Range(-spawnerWidth, spawnerHeight) + ")");

        return spawnTile.transform.position;
    }
}
