using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;

    private Vector3 velocity = Vector3.zero;
    private bool isFollowing = true;

    void FixedUpdate()
    {
        if (isFollowing)
            FollowTarget();
    }

    private void FollowTarget()
    {
        var offsetToTarget = target.position - transform.position;
        var positionChange = offsetToTarget * moveSpeed * Time.deltaTime;
        positionChange.z = 0;
        transform.position += positionChange;
    }

    public void Toggle(bool isFollowing)
    {
        this.isFollowing = isFollowing;
    }
}
