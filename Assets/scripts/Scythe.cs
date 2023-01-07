using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Follower))]
public class Scythe : ICollidable
{
    [SerializeField] private PolygonCollider2D attackArea;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip swingAnimation;

    [SerializeField] private float radius = 1;
    [SerializeField]
    [Range(0, 360)]
    private float angleInDegrees = 30;

    [SerializeField] private Vector3 forwardDirection;

    [SerializeField] private float attackDuration = 1.5f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Follower follower;
    private bool isAttacking = false;
    private bool isOnCooldown = false;

    private List<Animal> animalsInRange = new List<Animal>();

    private void Awake()
    {
        this.follower = GetComponent<Follower>();
        InitializeAttackArea();
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
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
        follower.Toggle(false);
        animator.SetBool("isAttacking", true);
        animator.speed = swingAnimation.length / attackDuration;
        yield return new WaitForSeconds(attackDuration);
        animator.SetBool("isAttacking", false);
        follower.Toggle(true);
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
    }
}
