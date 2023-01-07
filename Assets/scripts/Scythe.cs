using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Follower))]
public class Scythe : MonoBehaviour
{

    [SerializeField] private float radius = 1;
    [SerializeField]
    [Range(0, 360)]
    private float angleInDegrees = 30;

    [SerializeField] private Vector3 forwardDirection;

    [SerializeField] private float attackDuration = 1.5f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Follower follower;
    private bool isAttacking = false;

    private void Awake()
    {
        this.follower = GetComponent<Follower>();
    }

    private void FixedUpdate()

    {
        LookAtMouse();
    }

    private Vector3 GetDirectionToMouse()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return (mousePosition - transform.position).normalized;
    }

    private void LookAtMouse()
    {
        var directionToMouse = GetDirectionToMouse();
        var angle = Vector3.SignedAngle(forwardDirection, directionToMouse, Vector3.forward);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private (Vector3, Vector3) GetAttackArea()
    {
        float rotationAngle = 0.5f * angleInDegrees;
        Quaternion rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        Quaternion oppositeRotation = Quaternion.AngleAxis(rotationAngle, Vector3.back);

        return (rotation * transform.up * radius, oppositeRotation * transform.up * radius);
    }

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        follower.Toggle(false);
        yield return new WaitForSeconds(attackDuration);
        follower.Toggle(true);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        var (firstBoundary, secondBoundary) = GetAttackArea();

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + firstBoundary);
        Gizmos.DrawLine(transform.position, transform.position + secondBoundary);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + forwardDirection.normalized);
    }
}