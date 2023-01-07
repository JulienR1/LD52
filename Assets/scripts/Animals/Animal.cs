using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Animal : MonoBehaviour
{
    protected string type;
    protected float speed;
    protected float directionChangeTime;

    private float lastDirChangeTime;
    private Vector2 movPerSec;

    public virtual void Die()
    {
        print("Animal is dead");
        Destroy(this.gameObject);
    }
    protected virtual void Start()
    {
        lastDirChangeTime = 0f;
        CalculateNewMovVector();
    }
    private void CalculateNewMovVector()
    {
        Vector2 movDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movPerSec = movDir * speed;
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + (movPerSec.x * Time.deltaTime),
        transform.position.y + (movPerSec.y * Time.deltaTime));
    }
    protected virtual void Roam()
    {
        if(Time.time - lastDirChangeTime > directionChangeTime)
        {
            lastDirChangeTime= Time.time;
            CalculateNewMovVector();
        }
        Move();
    }

    public void OnTriggerEnter2D(Collision2D collision)
    {
        print("collison");
        lastDirChangeTime = Time.time;
        CalculateNewMovVector();
        Move();
    }

    protected virtual void Update()
    {
        Roam();
    }
}