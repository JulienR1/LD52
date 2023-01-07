using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollider : MonoBehaviour
{
    public PlayerController controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnCollision(other);
    }

    private void OnCollision(Collider2D other)
    {
        var distance = other.Distance(GetComponent<Collider2D>());
        var difference = distance.pointB - distance.pointA;
        controller.OnCollision(difference);
    }
}
