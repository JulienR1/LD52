using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialMoveSpeed = 10;
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = initialMoveSpeed;
    }

    void Update()
    {
        var pos = InputManager.GetAxis(Axis.Vertical) + InputManager.GetAxis(Axis.Horizontal);
        var velocity = pos * moveSpeed * Time.deltaTime;
        this.transform.position += new Vector3(velocity.x, velocity.y, 0);
    }
}
