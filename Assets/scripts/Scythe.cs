using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : ICollidable
{
    [SerializeField] private PolygonCollider2D attackArea;

    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform weaponGraphics;
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
        var spinAroundPosition = new Vector3(spinAround.position.x, spinAround.position.y, 0);
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        if ((mousePosition - spinAroundPosition).sqrMagnitude < mouseInactivityRadius * mouseInactivityRadius)
            return;

        var directionToMouse = (mousePosition - spinAroundPosition).normalized;
        var zAngle = Vector3.SignedAngle(Vector3.right, directionToMouse, Vector3.forward);

        weaponHolder.rotation = Quaternion.Euler(0, 0, zAngle);
        weaponGraphics.rotation = Quaternion.Euler(weaponGraphics.rotation.x, Input.mousePosition.x < Screen.width / 2f ? 180 : weaponGraphics.rotation.y, weaponGraphics.rotation.z);
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
            animalsInRange[animalsInRange.Count - i - 1].Damage();
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
