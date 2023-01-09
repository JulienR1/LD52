using UnityEngine;

public class Animal : ICollidable
{
    [SerializeField] public AnimalData specs;
    [SerializeField] private SpriteRenderer graphics;

    private int currentHealth;
    private float lastDirChangeTime;
    private Vector2 movPerSec;

    private bool isPanicking = false;
    private float panickEndTime;

    public virtual void Damage()
    {
        currentHealth--;
        isPanicking = true;
        panickEndTime = Time.time + specs.panickDuration;
        if (currentHealth > 0)
            return;

        createCorpse();

        for (int i = 0; i < specs.soulsDropped; i++)
            Instantiate(specs.spiritPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    protected virtual void Start()
    {
        lastDirChangeTime = 0f;
        graphics.sprite = specs.sprite;
        currentHealth = specs.healthPoint;
        CalculateNewMovVector();
    }
    private void CalculateNewMovVector()
    {
        float speedFactor = Random.Range(0.5f, 1.0f);
        float randomizedSpeed = specs.speedRange.x + (specs.speedRange.y - specs.speedRange.x) * speedFactor;
        float remainStaticProbability = Random.Range(0.0f, 1.0f);
        float newDirectionSmoothing = 0.01f;

        Stop();

        if (remainStaticProbability > 0.3f)
        {
            Vector2 movDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            movPerSec = Vector2.Lerp(movPerSec, movDir * randomizedSpeed, Time.deltaTime / newDirectionSmoothing);
        }
    }

    private void Move()
    {
        transform.position += new Vector3(movPerSec.x, movPerSec.y, 0) * Time.deltaTime * (isPanicking ? specs.panickMoveSpeedFactor : 0);
    }

    private void Stop()
    {
        movPerSec = Vector2.zero;
    }

    protected virtual void Roam()
    {
        if (Time.time - lastDirChangeTime > specs.directionChangeTime)
        {
            lastDirChangeTime = Time.time;
            CalculateNewMovVector();
        }
        Move();
    }

    protected virtual void Update()
    {
        if (Time.time >= panickEndTime)
            isPanicking = false;

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

    private void createCorpse()
    {
        if (!GameObject.Find("corpses"))
            new GameObject("corpses");

        GameObject corpse = new GameObject("corpse");
        SpriteRenderer sr = corpse.AddComponent<SpriteRenderer>();
        sr.sprite = specs.deadSprites[Random.Range(0, specs.deadSprites.Count)];
        sr.sortingOrder = 0;
        corpse.transform.position = this.transform.position;
        corpse.transform.localScale = 2 * this.transform.localScale;
        corpse.transform.parent = GameObject.Find("corpses").transform;
    }
}