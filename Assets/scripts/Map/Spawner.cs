using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Animal animalPrefab;
    public AnimalData[] animalList;

    public Vector2 spawnRangeMinMax;
    public int animalCountStart;
    public int maxAnimalsInSpawner = 10;
    public float spawnRateInSec = 5f;

    private int spawnerId;
    private float lastSpawnTime;

    public void Init()
    {
        for (int i = 0; i < animalCountStart; i++)
            SpawnAnimal();

        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnRateInSec)
        {
            SpawnAnimal();
            lastSpawnTime = Time.time;
        }
    }

    public void SetSpawnerId(int id)
    {
        spawnerId = id;
    }

    private void SpawnAnimal()
    {
        if (transform.childCount >= maxAnimalsInSpawner)
            return;

        var animal = Instantiate(animalPrefab.gameObject, GetRandomSpawnPosition(), Quaternion.identity);
        animal.GetComponent<Animal>().specs = animalList[Random.Range(0, animalList.Length)];
        animal.transform.SetParent(this.transform);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float angle = 2 * Mathf.PI * Random.value;
        float radius = Random.value * (spawnRangeMinMax.y - spawnRangeMinMax.x) + spawnRangeMinMax.x;
        return transform.position + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRangeMinMax.x);
        Gizmos.DrawWireSphere(transform.position, spawnRangeMinMax.y);
    }
}
