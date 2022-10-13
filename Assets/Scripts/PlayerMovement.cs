using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject OriginPoint;

    private float moveSpeed;
    private float maxDistance;

    private float bulletCooldownTimeLeft;
    public float bulletCooldownTime;

    Vector2 movement;

    World world;


    void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        maxDistance = world.maxDistance;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(world.health > 0)
        {
            Movement();
            TeleportOnEdges();
            moveSpeed = world.playerMovementSpeed;
            Laser();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Laser()
    {
        //COOLDOWN CAN NOT GO UNDER 0
        if (bulletCooldownTimeLeft < 0)
        {
            bulletCooldownTimeLeft = 0;
        }
        //COOLDOWN SET
        if (bulletCooldownTimeLeft > 0)
        {
            bulletCooldownTimeLeft -= Time.deltaTime;
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && bulletCooldownTimeLeft == 0)
        {
            Instantiate(world.Bullet, transform.position, Quaternion.identity, this.transform);
            bulletCooldownTimeLeft = bulletCooldownTime;
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy2" || col.tag == "Boss" || col.tag == "EnemyBullet")
        {
            world.health -= 1;
            world.healthSlider.value -= 1;
            Debug.Log("DMG");
            Debug.Log(col.tag);
        }
    }
}