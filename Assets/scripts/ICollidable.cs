using UnityEngine;

public abstract class ICollidable : MonoBehaviour
{
    public abstract void OnCollision(Vector2 collisionDifference);
}
