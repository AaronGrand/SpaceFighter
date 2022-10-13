using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector2 player;

    [SerializeField] private float moveSpeed = 5;

    private float maxDistance;

    private Vector2 movement;

    World world;

    private void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        maxDistance = world.maxDistance;

        player = GameObject.Find("Player Object").transform.position;

        player -= new Vector2(transform.position.x, transform.position.y);

        movement = new Vector2(player.x, player.y) * 100;
    }

    private void Update()
    {
        if (world.health > 0)
        {
            if (transform.position.y >= maxDistance || transform.position.y <= -maxDistance || transform.position.x >= maxDistance || transform.position.x <= -maxDistance)
                Destroy(gameObject);

            MovePos();
        }
    }

    private void MovePos()
    {
        if (world.health > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, movement, moveSpeed * Time.deltaTime * 30);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            Destroy(gameObject);
    }
}
