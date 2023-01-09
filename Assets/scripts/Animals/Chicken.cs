using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Chicken : Animal
{
    Chicken()
    {
        type = "chicken";
        minSpeed = 1f;
        maxSpeed = 1.5f;
        directionChangeTime = 1f;
        deadSprite = "skeleton-0";
    }
}
