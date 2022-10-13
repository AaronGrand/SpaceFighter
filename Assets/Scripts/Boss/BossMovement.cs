using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 target;
    private Slider HealthSlider;
    private Vector2 player;
    public GameObject EnemyLaser;

    private float maxDistance;

    private float moveSpeed;
    public int health;

    private float lcdl;
    private float lcd = 1f;

    World world;

    void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        player = GameObject.Find("Player Object").transform.position;
        HealthSlider = world.BossHealth;
        HealthSlider.enabled = true;
        maxDistance = world.maxDistance;
        moveSpeed = world.enemyMovementSpeed;

        rb = GetComponent<Rigidbody2D>();

        target.x = Random.Range(-maxDistance, maxDistance);
        target.y = Random.Range(-maxDistance, maxDistance);
        health = 9;

        HealthSlider.maxValue = health;
        HealthSlider.value = health;
    }

    void Update()
    {
        if (world.health > 0)
        {
            MovePos();
            TeleportOnEdges();
            LaserAttack();
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
            HealthSlider.value = health;
        }
        else if (col.tag == "Bullet" && health <= 0)
        {
            Destroy(gameObject);
            world.bossDied = true;
            HealthSlider.enabled = false;
        }
    }

    void LaserAttack()
    {
        if (lcdl < 0)
        {
            lcdl = 0;
        }

        if (lcdl > 0)
        {
            lcdl -= Time.deltaTime;
        }

        if(lcdl == 0)
        {
            Instantiate(EnemyLaser, transform.position, Quaternion.identity);
            lcdl = lcd;
        }
    }
}
