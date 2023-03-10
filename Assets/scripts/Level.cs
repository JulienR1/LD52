using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LD52/Level")]
public class Level : ScriptableObject
{
    [Header("Map")]
    public int width;
    public int height;

    [Header("Animals")]
    public List<EnemySpawner> spawners;
}

[System.Serializable]
public class EnemySpawner
{
    public List<AnimalData> animals;
    public Vector2 spawnRangeMinMax;
    public int initialSpawnCount;
    public int maxAnimalCount;
    public float spawnRateInsec;
}