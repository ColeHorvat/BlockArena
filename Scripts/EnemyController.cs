using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    

    public Transform target;
    public float speed = 400f;
    float nextWayPointDistance = 3f;


    PlayerController playerController;
    public GameObject player;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb2d;
    BoxCollider2D enemyCollider;
    SpriteRenderer spriteRenderer;

    public AudioSource damage;
    

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = player.transform;

        InvokeRepeating("UpdatePath", 0f, .5f);
        seeker.StartPath(rb2d.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null) return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2d.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb2d.AddForce(force);

        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance) currentWaypoint++;

    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone()) seeker.StartPath(rb2d.position, target.position, OnPathComplete);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerController.playerHealth--;

            spriteRenderer.enabled = false;
            enemyCollider.enabled = false;
            damage.Play();
            Destroy(gameObject, damage.clip.length);
            
            
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(other.collider, enemyCollider);
        }
    }
}
