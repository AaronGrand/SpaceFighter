using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Bullet;
    public GameObject FirstBoss;

    public Slider experienceSlider;
    public Slider healthSlider;
    public Slider BossHealth;

    public float maxDistance = 50f;

    public int difficulty;
    public int points;

    private float randomEnemyNumber;
    private Vector2 enemySpawnPosition;
    private float enemyCooldownTimeLeft;
    [SerializeField] private float enemyCooldownTime = 1f;

    public int experience;
    public int experienceNeeded;

    public int maxHealth = 10;
    public int health;

    public float enemyMovementSpeed = 0.2f;
    public float playerMovementSpeed = 10f;

    private float pcdl;
    private float pcd = 1;

    private int bonusPoints;

    public int bonusKillPoints;

    private float xpcdl;
    private float xpcd = 2;

    public bool bossFight = false;
    private bool isBossSpawned;
    public bool bossDied = false;

    //LATER FOR SAVING

    PlayerMovement player;

    private void Awake()
    {

    }

    private void Start()
    {
        player = GameObject.Find("Player Object").GetComponent<PlayerMovement>();

        enemyMovementSpeed = 0.2f;
        playerMovementSpeed = 10f;

        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        difficulty = 1;
        isBossSpawned = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(1);

        if(health > 0)
        {
            SetDifficulty();
            Experience();
            ExperienceCooldown();
            PointsCooldown();
            
            SpawnEnemy();
        }
        //YOU DIED
        if(health <= 0)
        {
            //ENTER DEATH SCREEN

            //SAVE POINTS IN DATA FILE

            
            //SHOW BEST 5 SCORES WITH NAME
        }
    }

    void ExperienceCooldown()
    {
        if (xpcdl <= 0)
            xpcdl = 0;
        if (xpcdl > 0)
            xpcdl -= Time.deltaTime;
        if (xpcdl == 0 && !bossFight)
        {
            experience++;
            xpcdl = xpcd;
        }
    }
    void PointsCooldown()
    {
        if (pcdl <= 0)
            pcdl = 0;
        if (pcdl > 0)
            pcdl -= Time.deltaTime;
        if (pcdl == 0 && !bossFight)
        {
            points++;
            pcdl = pcd;
        }
    }

    void Experience()
    {
        if(experience >= experienceNeeded)
        {
            if (!bossFight)
            {
                bossFight = true;
                if (!isBossSpawned)
                {
                    Instantiate(FirstBoss, new Vector2(0, maxDistance), Quaternion.identity, this.transform);
                    isBossSpawned = true;
                }
            }
            if (bossDied)
            {
                difficulty++;
                points += bonusPoints;
                experience -= experienceNeeded;
                experienceSlider.value = experience;
                bossFight = false;
                bossDied = false;
                isBossSpawned = false;
            }
        }
    }

    void SetDifficulty()
    {
        switch (difficulty)
        {
            case 1:
                enemyCooldownTime = 4f;
                experienceNeeded = 10;
                experienceSlider.maxValue = experienceNeeded;
                bonusPoints = 0;
                bonusKillPoints = 0;
                player.bulletCooldownTime = 1;
                break;
            case 2:
                enemyCooldownTime = 3.5f;
                experienceNeeded = 20;
                experienceSlider.maxValue = experienceNeeded;
                enemyMovementSpeed = 0.25f;
                playerMovementSpeed = 10f;
                bonusPoints = 10;
                bonusKillPoints = 5;
                player.bulletCooldownTime = 0.9f;
                break;
            case 3:
                enemyCooldownTime = 3.25f;
                experienceNeeded = 30;
                experienceSlider.maxValue = experienceNeeded;
                enemyMovementSpeed = 0.3f;
                playerMovementSpeed = 12.5f;
                bonusPoints = 50;
                bonusKillPoints = 10;
                player.bulletCooldownTime = 0.8f;
                break;
            case 4:
                enemyCooldownTime = 3f;
                experienceNeeded = 40;
                experienceSlider.maxValue = experienceNeeded;
                enemyMovementSpeed = 0.35f;
                playerMovementSpeed = 15f;
                bonusPoints = 100;
                bonusKillPoints = 25;
                player.bulletCooldownTime = 0.7f;
                break;
            case 5:
                enemyCooldownTime = 2.5f;
                experienceNeeded = 50;
                experienceSlider.maxValue = experienceNeeded;
                enemyMovementSpeed = 0.40f;
                playerMovementSpeed = 17.5f;
                bonusPoints = 250;
                bonusKillPoints = 50;
                player.bulletCooldownTime = 0.6f;
                break;
            case 6:
                enemyCooldownTime = 2f;
                experienceNeeded = 60;
                experienceSlider.maxValue = experienceNeeded;
                enemyMovementSpeed = 0.45f;
                playerMovementSpeed = 20f;
                bonusPoints = 500;
                bonusKillPoints = 100;
                player.bulletCooldownTime = 0.5f;
                break;
            case 7:
                enemyCooldownTime = 1.5f;
                experienceNeeded = 70;
                experienceSlider.maxValue = experienceNeeded;
                enemyMovementSpeed = 0.50f;
                playerMovementSpeed = 22.5f;
                bonusPoints = 1000;
                bonusKillPoints = 500;
                player.bulletCooldownTime = 0.4f;
                break;
            case 8:
                enemyCooldownTime = 1f;
                experienceNeeded = 80;
                experienceSlider.maxValue = experienceNeeded;
                enemyMovementSpeed = 0.55f;
                playerMovementSpeed = 25f;
                bonusPoints = 5000;
                bonusKillPoints = 1000;
                player.bulletCooldownTime = 0.3f;
                break;
            case 9:
                enemyCooldownTime = 0.75f;
                experienceSlider.maxValue = 0;
                enemyMovementSpeed = 0.60f;
                playerMovementSpeed = 30f;
                bonusPoints = 10000;
                bonusKillPoints = 5000;
                player.bulletCooldownTime = 0.2f;
                break;
        }
    }

    void SpawnEnemy()
    {
        if (enemyCooldownTimeLeft < 0)
        {
            enemyCooldownTimeLeft = 0;
        }
        //COOLDOWN SET
        if (enemyCooldownTimeLeft > 0)
        {
            enemyCooldownTimeLeft -= Time.deltaTime;
        }

        //Set SpawnPosition
        randomEnemyNumber = Mathf.FloorToInt(Random.Range(0, 4));
        switch (randomEnemyNumber)
        {
            //Spawn Right
            case 0:
                enemySpawnPosition = new Vector2(50, Random.Range(-50, 50));
                break;
            //Spawn Left
            case 1:
                enemySpawnPosition = new Vector2(-50, Random.Range(-50, 50));
                break;
            //Spawn Top
            case 2:
                enemySpawnPosition = new Vector2(Random.Range(-50, 50), 50);
                break;
            //Spawn Bottom
            case 3:
                enemySpawnPosition = new Vector2(Random.Range(-50, 50), -50);
                break;
        }
        
        //Spawn Enemy
        if(enemyCooldownTimeLeft == 0 && !bossFight)
        {
            Instantiate(Enemy, enemySpawnPosition, Quaternion.identity, this.transform);
            enemyCooldownTimeLeft = enemyCooldownTime;
        }
    }


}