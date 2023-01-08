using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Animal : ICollidable
{
    [SerializeField] private GameObject spiritPrefab;

    protected string type;
    protected float maxSpeed;
    protected float minSpeed;
    protected float directionChangeTime;

    private float lastDirChangeTime;
    private Vector2 movPerSec;

    public virtual void Die()
    {
        print("Animal is dead");
        Instantiate(spiritPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    protected virtual void Start()
    {
        lastDirChangeTime = 0f;
        CalculateNewMovVector();
    }
    private void CalculateNewMovVector()
    {
        float speedFactor = Random.Range(0.5f, 1.0f);
        float randomizedSpeed = minSpeed + (maxSpeed - minSpeed) * speedFactor;
        float remainStaticProbability = Random.Range(0.0f, 1.0f);
        float newDirectionSmoothing = 0.01f;

        Stop();

        if (remainStaticProbability > 0.3f)
        {
            Vector2 movDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            movPerSec = Vector2.Lerp(movPerSec, movDir * randomizedSpeed, Time.deltaTime / newDirectionSmoothing);

            // movPerSec = movDir * randomizedSpeed;
        }
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + (movPerSec.x * Time.deltaTime),
        transform.position.y + (movPerSec.y * Time.deltaTime));
    }

    private void Stop()
    {
        movPerSec = Vector2.zero;
    }

    protected virtual void Roam()
    {
        if (Time.time - lastDirChangeTime > directionChangeTime)
        {
            lastDirChangeTime = Time.time;
            CalculateNewMovVector();
        }
        Move();
    }

    // public void OnTriggerEnter2D(Collision2D collision)
    // {
    //     print("collison");
    //     lastDirChangeTime = Time.time;
    //     CalculateNewMovVector();
    //     Move();
    // }

    protected virtual void Update()
    {
        Roam();
    }

    public override void OnCollision(Vector2 collisionDifference, GameObject other)
    {
        int layermask = LayerMask.NameToLayer("player");
        if (other.tag != "Puit" && other.layer != layermask)
        {
            this.transform.Translate(-collisionDifference);
            CalculateNewMovVector();
        }
    }

    public override void OnCollisionEnd(GameObject other) { }
}