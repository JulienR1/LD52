using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : ICollidable
{
    [SerializeField] private PolygonCollider2D attackArea;
    [SerializeField] private Transform weaponHolder;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip swingAnimation;

    [SerializeField] private float radius = 1;
    [SerializeField, Range(0, 360)]
    private float angleInDegrees = 30;

    [SerializeField] private Vector3 forwardDirection;
    [SerializeField] private float mouseInactivityRadius;
    [SerializeField] private Transform spinAround;

    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float attackCooldown = 0.2f;

    private bool isAttacking = false;
    private bool isOnCooldown = false;

    private List<Animal> animalsInRange = new List<Animal>();

    private void Awake()
    {
        InitializeAttackArea();
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
            LookAtMouse();
    }

    private void LookAtMouse()
    {

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if ((mousePosition - spinAround.position).sqrMagnitude < mouseInactivityRadius * mouseInactivityRadius)
            return;

        var directionToMouse = (mousePosition - spinAround.position).normalized;
        var angle = Vector3.SignedAngle(forwardDirection, directionToMouse, Vector3.forward);

        // if mouse is on the left side of the player, rotate the scythe in the opposite direction
        var xAnle = 0;
        if (mousePosition.x < spinAround.position.x) {xAnle = 180; angle = -angle;}
        weaponHolder.rotation = Quaternion.Euler(xAnle, 0, angle);
    }

    private (Vector3, Vector3) GetAttackArea()
    {
        float rotationAngle = 0.5f * angleInDegrees;
        Quaternion rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        Quaternion oppositeRotation = Quaternion.AngleAxis(rotationAngle, Vector3.back);

        return (rotation * transform.up * radius, oppositeRotation * transform.up * radius);
    }

    private void InitializeAttackArea()
    {
        var (firstSegment, secondSegment) = GetAttackArea();
        attackArea.points = new Vector2[] { Vector2.zero, firstSegment, secondSegment };
    }

    public void Attack()
    {
        if (!isAttacking && !isOnCooldown)
            StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.speed = swingAnimation.length / attackDuration;
        yield return new WaitForSeconds(attackDuration);
        animator.SetBool("isAttacking", false);
        isAttacking = false;
        isOnCooldown = true;

        for (int i = 0; i < animalsInRange.Count; i++)
        {
            animalsInRange[animalsInRange.Count - i - 1].Die();
        }

        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    public override void OnCollision(Vector2 collisionDifference, GameObject other)
    {
        var animal = other.GetComponentInParent<Animal>();
        if (animal != null && animalsInRange.Find((currentAnimal) => GameObject.ReferenceEquals(animal.gameObject, currentAnimal.gameObject)) == null)
            animalsInRange.Add(animal);
    }

    public override void OnCollisionEnd(GameObject other)
    {
        var animal = other.GetComponentInParent<Animal>();
        RemoveAnimalFromRange(animal);
    }

    private void RemoveAnimalFromRange(Animal animal)
    {
        var index = animalsInRange.FindIndex((currentAnimal) => GameObject.ReferenceEquals(currentAnimal.gameObject, animal.gameObject));
        if (index >= 0)
        {
            animalsInRange.RemoveAt(index);
        }
    }

    private void OnDrawGizmos()
    {
        var (firstBoundary, secondBoundary) = GetAttackArea();

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + firstBoundary);
        Gizmos.DrawLine(transform.position, transform.position + secondBoundary);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + forwardDirection.normalized);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spinAround.position, mouseInactivityRadius);
    }
}
