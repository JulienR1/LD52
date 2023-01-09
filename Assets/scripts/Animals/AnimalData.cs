using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalData", menuName = "LD52/AnimalData", order = 0)]
public class AnimalData : ScriptableObject
{
    [Header("Assets")]
    public Sprite sprite;
    public GameObject spiritPrefab;
    public List<Sprite> deadSprites;

    [Header("Settings")]
    public Vector2 speedRange;
    public float directionChangeTime;
}