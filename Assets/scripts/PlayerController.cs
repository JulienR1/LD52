using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float acceleration = 1;
    private float speedFactor = 1;

    private Vector3 velocity = Vector3.zero;

    public void ChangeSpeedFactor()
    {
        // TODO
    }

    private void FixedUpdate()
    {
        var input = InputManager.GetAxis(Axis.Horizontal) + InputManager.GetAxis(Axis.Vertical);
        var targetVelocity = input * speedFactor * moveSpeed * Time.deltaTime;

        velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
        this.transform.Translate(velocity.x, velocity.y, 0);
    }

    public void OnCollision(Vector2 collisionDifference)
    {
        this.transform.Translate(-collisionDifference);
    }
}
