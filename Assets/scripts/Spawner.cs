using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject chickenPrefab;
    //public GameObject cowPrefab
    //public int cowPercent;
    public int animalCountStart;
    public int chickenPercent;
    public float spawnInterval;
    public int[] spawnPoints = {-5,-4,-3,-2,2,3,4};
    private float lastSpawn;

    // Start is called before the first frame update
    void Start()
    {

        for(int i=0;i<animalCountStart;i++){
            GameObject animalSpawn = GameObject.Find("(" + chooseTile() + ", " + chooseTile() + ")");
            SpawnAnimal(animalSpawn);
        }
        lastSpawn = Time.time;
    }

    private int chooseTile(){
        int choice = Random.Range(0, spawnPoints.Length);
        return spawnPoints[choice];
    }

    private void SpawnAnimal(GameObject spawnLocation){
        int totalChance = chickenPercent;
        int randomNum = Random.Range(1, totalChance);
        if (randomNum >= 1 || randomNum <= chickenPercent){
            Instantiate(chickenPrefab, spawnLocation.transform.position, Quaternion.identity);
        }
        // else if(randomNum >= chickenPercent+1 || randomNum <= cowPercent){
        //     Instantiate(cowPrefab, spawnLocation.transform.position, Quaternion.identity);
        // }
        // else{
        //     Instantiate(pigPrefab, spawnLocation.transform.position, Quaternion.identity);
        // }

    }

    private void SpawnOverTime(){
        if(Time.time - lastSpawn > spawnInterval){
            GameObject animalSpawn = GameObject.Find("(" + chooseTile() + ", " + chooseTile() + ")");
            SpawnAnimal(animalSpawn);
            lastSpawn = Time.time;
        }
    }    

    // Update is called once per frame
    void Update()
    {
        SpawnOverTime();
    }

    
}
