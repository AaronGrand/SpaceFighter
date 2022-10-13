using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 target;

    private float maxDistance;

    private float moveSpeed;
    public int health;

    World world;

    void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        maxDistance = world.maxDistance;
        moveSpeed = world.enemyMovementSpeed;

        rb = GetComponent<Rigidbody2D>();

        target.x = Random.Range(-maxDistance, maxDistance);
        target.y = Random.Range(-maxDistance, maxDistance);
        health = 2;
    }

    void Update()
    {
        if (world.health > 0)
        {
            MovePos();
            TeleportOnEdges();
        }
    }

    private void TeleportOnEdges()
    {
        if (transform.position.y > maxDistance)
            transform.position = new Vector3(transform.position.x, -maxDistance, 0);
        if (transform.position.y < -maxDistance)
            transform.position = new Vector3(transform.position.x, maxDistance, 0);
        if (transform.position.x > maxDistance)
            transform.position = new Vector3(-maxDistance, transform.position.y, 0);
        if (transform.position.x < -maxDistance)
            transform.position = new Vector3(maxDistance, transform.position.y, 0);
    }
    
    private void MovePos()
    {
        rb.MovePosition(rb.position + target * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet" && health > 0)
        {
            health -= 1;
        }
        else if (col.tag == "Bullet" && health <= 0)
        {
            Destroy(gameObject);
            if (!world.bossFight)
            {
                world.points += 5 + world.bonusKillPoints;
                world.experience += 1;
            }
        }
    }
}
