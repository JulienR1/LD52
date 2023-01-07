using System.Collections;
using System.Collections.Generic;
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
        transform.position += offsetToTarget * moveSpeed * Time.deltaTime;
    }

    public void Toggle(bool isFollowing)
    {
        this.isFollowing = isFollowing;
    }
}
