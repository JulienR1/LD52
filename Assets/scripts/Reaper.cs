using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private Scythe scythe;

    void Start()
    {

    }

    void Update()
    {
        if (InputManager.GetActionDown(KeyAction.Attack))
        {
            scythe.Attack();
        }
    }
}
