using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    World world;
    PlayerMovement player;
    public TextMeshProUGUI difficulty;
    public TextMeshProUGUI points;
    public TextMeshProUGUI experience;
    public TextMeshProUGUI health;
    public TextMeshProUGUI moveSpeed;
    public TextMeshProUGUI reloadTime;


    void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        player = GameObject.Find("Player Object").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        difficulty.text = "Difficulty : " + world.difficulty;
        points.text = "Points : ";
        points.text += "\n";
        points.text += world.points;
        experience.text = "Experience : " + world.experience + "/" + world.experienceNeeded;
        health.text = "Health : " + world.health + "/" + world.maxHealth;
        moveSpeed.text = "Speed : " + "\n" + world.playerMovementSpeed;
        reloadTime.text = "ReloadTime : " + "\n" + player.bulletCooldownTime;
    }
}
