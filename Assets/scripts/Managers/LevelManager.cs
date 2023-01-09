using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject well;
    public Level[] levels;
    public Map mapBase;

    public Animal animalPrefab;
    public Transform levelHolder;

    private int spawnerId = -1;
    private int levelIndex = -1;


    void Start()
    {
        InstantiatePlayer();
        LoadNextLevel();
    }

    private void InstantiatePlayer()
    {
        Instantiate(well, Vector3.zero, Quaternion.identity);
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    public void LoadNextLevel()
    {
        for (int i = 0; i < levelHolder.childCount; i++)
            Destroy(levelHolder.GetChild(levelHolder.childCount - 1 - i));

        if (levels.Length > 0)
        {
            levelIndex = Mathf.Clamp(levelIndex + 1, 0, levels.Length - 1);
            InstantiateLevel(levels[levelIndex]);
        }
    }

    private void InstantiateLevel(Level level)
    {
        var map = Instantiate(mapBase, Vector3.zero, Quaternion.identity);
        map.transform.SetParent(levelHolder);
        map.levelWidth = level.width;
        map.levelHeight = level.height;
        map.Init();

        foreach (var spawnerData in level.spawners)
        {
            spawnerId++;
            var obj = new GameObject("Spawner");
            obj.transform.SetParent(map.transform);
            var spawner = obj.AddComponent<Spawner>();
            spawner.animalPrefab = animalPrefab;
            spawner.animalList = spawnerData.animals.ToArray();
            spawner.spawnRangeMinMax = spawnerData.spawnRangeMinMax;
            spawner.animalCountStart = spawnerData.initialSpawnCount;
            spawner.maxAnimalsInSpawner = spawnerData.maxAnimalCount;
            spawner.spawnRateInSec = spawnerData.spawnRateInsec;
            spawner.SetSpawnerId(spawnerId);
            spawner.Init();
        }
    }
}
