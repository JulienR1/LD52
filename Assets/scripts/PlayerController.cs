using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ICollidable
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

        var graphics = this.transform.Find("graphics");

        if (Input.mousePosition.x < Screen.width / 2)
            graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.Euler(0, 180, 0), 0.2f);
        else
            graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.Euler(0, 0, 0), 0.2f);

        this.transform.Translate(velocity.x, velocity.y, 0);
    }

    public override void OnCollision(Vector2 collisionDifference, GameObject other)
    {
        if (other.tag != "Puit")
        {
            this.transform.Translate(-collisionDifference);
        }
    }

    public override void OnCollisionEnd(GameObject other) { }
}
