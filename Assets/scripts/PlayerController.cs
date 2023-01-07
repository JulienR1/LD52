using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    private float speedFactor = 1;

    public void ChangeSpeedFactor()
    {
        // TODO
    }

    private void FixedUpdate()
    {
        var input = InputManager.GetAxis(Axis.Horizontal) + InputManager.GetAxis(Axis.Vertical);
        var velocity = input * speedFactor * moveSpeed * Time.deltaTime;
        this.transform.Translate(velocity.x, velocity.y, 0);
    }

    public void OnCollision(Vector2 collisionDifference)
    {
        this.transform.Translate(-collisionDifference);
    }
}
