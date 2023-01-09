using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Spirit : MonoBehaviour
{
    [SerializeField] private AnimationClip spawnAnimation;
    [SerializeField] private AnimationClip dieAnimation;
    [SerializeField] private float minimumDistance;
    [SerializeField] private float acceleration;
    [SerializeField, Range(0, 1)] private float drunkFactor;
    [SerializeField] private float dampingFactor;

    private Puit well;
    private Reaper reaper;
    private Animator animator;

    private bool canMove = true;
    private bool isSpawned = false;
    private bool isSacrificed = false;

    private Vector3 velocity = Vector3.zero;
    private float seed;

    private void Start()
    {
        seed = Random.Range(0, 10000000);

        well = FindObjectOfType<Puit>();
        reaper = FindObjectOfType<Reaper>();
        animator = GetComponent<Animator>();
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (isSacrificed)
            Follow(well.transform);
        else if (isSpawned)
            Follow(reaper.transform);
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnAnimation?.length ?? 0);
        isSpawned = true;
    }

    public void Sacrifice()
    {
        isSacrificed = true;
        minimumDistance = 0.25f;
    }

    private void Follow(Transform target)
    {
        var offsetToTarget = target.position - transform.position;
        var regularDirection = (1 - drunkFactor) * offsetToTarget;
        var drunkDirection = drunkFactor * GetDrunkDirection();

        var oldVelocity = velocity;
        var dot = Vector3.Dot(regularDirection, oldVelocity.normalized);

        if (offsetToTarget.sqrMagnitude > minimumDistance * minimumDistance)
            velocity += regularDirection * acceleration * Time.deltaTime;
        else
            OnReachTarget();

        if (velocity.sqrMagnitude > minimumDistance)
            velocity += dampingFactor * (dot - 1) * oldVelocity * Time.deltaTime;
        velocity += drunkDirection * acceleration * Time.deltaTime;

        if (canMove)
            transform.position += velocity * Time.deltaTime;
    }

    private void OnReachTarget()
    {
        if (isSacrificed)
        {
            canMove = false;
            velocity = Vector3.zero;
            StartCoroutine(DisappearCoroutine());
        }
    }


    private IEnumerator DisappearCoroutine()
    {
        well.Open();
        yield return new WaitForSeconds(dieAnimation?.length ?? 0);
        well.Close();
        GameManager.AddSouls(1);
        Destroy(gameObject);
    }

    private Vector3 GetDrunkDirection()
    {
        var rawPercent = Mathf.PerlinNoise1D(Time.time + seed);
        var percent = Mathf.Clamp01(rawPercent);
        var angle = percent * 2 * Mathf.PI;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + velocity.normalized);
    }
}
