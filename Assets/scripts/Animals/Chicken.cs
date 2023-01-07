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
        speed = 2;
        directionChangeTime = 3f;
    }
}
