using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;



    [SerializeField] private bool m_spawnForever; //Set to true if the spawner will spawn endless enemies
    [SerializeField] private bool m_spawnInRange; // Enemy will spawn at a point within the hitbox size of the Spawner
    [SerializeField] private bool m_spawnPositionSet; //Spawn Positions for the spawner are specific locations
    [SerializeField] private bool m_spawnerHasTrigger;
    [SerializeField] private Vector2 m_spawnPosition1;
    [SerializeField] private Vector2 m_spawnPosition2;
    [SerializeField] private Vector2 m_spawnPosition3;
    [SerializeField] private Vector2 m_spawnPosition4;  // Spawn Positions (up to 5 per spawner)
    [SerializeField] private Vector2 m_spawnPosition5;
    [SerializeField] private int m_spawnPositionCount; //How many spawn positions will we use?
    [SerializeField] private int m_spawnCount;           //The amount of creatures to spawn (ignored if spawnForever)
    [SerializeField] private float m_minimumSpawnTime;
    [SerializeField] private float m_maximumSpawnTime;
    
    public Transform SpawnerID;
    public GameObject spawnerHitbox;
    public GameObject triggerRange;
    public GameObject cognitivePlayer;
    private bool m_spawnerTriggered;

    private float m_timeUntilSpawn;
    private Vector2 m_spawnLocation;
    private int m_spawnLocationChosen;
    private Vector2 m_spawnerPosition;
    private Vector2 m_spawnerSize;
    private Vector2 m_minBoundary;
    private Vector2 m_maxBoundary;

    // Start is called before the first frame update
    void Start()
    {
        //Setup Spawn Boundaries IF Spawning in range
        if (m_spawnInRange)
        {
            m_minBoundary = spawnerHitbox.GetComponent<Renderer>().bounds.min;
            m_maxBoundary = spawnerHitbox.GetComponent<Renderer>().bounds.max;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_spawnerTriggered || m_spawnerHasTrigger == false)
        {
            m_timeUntilSpawn -= Time.deltaTime; //Slowly run down time
        }

        if (m_timeUntilSpawn <= 0)
        {
            if (m_spawnForever || m_spawnCount > 0)
            {
                //yaay
                SpawnEnemy();
                if (m_spawnCount > 0)
                {
                    m_spawnCount--;
                }
            }
            SetTimeUntilSpawn();
        }

        if (triggerRange != null) 
        {
            Vector2 minBoundary = triggerRange.GetComponent<Collider2D>().bounds.min;
            Vector2 maxBoundary = triggerRange.GetComponent<Collider2D>().bounds.max;
            Collider2D[] colliders = Physics2D.OverlapAreaAll(minBoundary, maxBoundary);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == cognitivePlayer)
                {
                    if (m_spawnerHasTrigger)
                    {
                        m_spawnerTriggered = true;
                    }
                    // Handle what happens when the target overlaps with the TriggerRange
                }
            }
        }
    }

    private void SetTimeUntilSpawn()
    {
        m_timeUntilSpawn = UnityEngine.Random.Range(m_minimumSpawnTime, m_maximumSpawnTime);
    }

    private void SpawnEnemy()
    {
        if (m_spawnInRange)
        {
            // Get random location to spawn!
            float randomX = UnityEngine.Random.Range(m_minBoundary.x, m_maxBoundary.x);
            float randomY = UnityEngine.Random.Range(m_minBoundary.y, m_maxBoundary.y);

            m_spawnLocation = new Vector2(randomX, randomY);
            GameObject newEnemy = Instantiate(m_enemyPrefab, m_spawnLocation, Quaternion.identity, SpawnerID);
            newEnemy.layer = 7;
            newEnemy.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Enemies");
            EnemyBehaviour enemyScript = newEnemy.GetComponent<EnemyBehaviour>();
        }
        else if (m_spawnPositionSet)
        {
            switch (m_spawnPositionCount)
            {
                case 1:
                    m_spawnLocationChosen = 1;
                    break;
                case 2:
                    m_spawnLocationChosen = UnityEngine.Random.Range(1, 3);
                    break;
                case 3:
                    m_spawnLocationChosen = UnityEngine.Random.Range(1, 4);
                    break;
                case 4:
                    m_spawnLocationChosen = UnityEngine.Random.Range(1, 5);
                    break;
                case 5:
                    m_spawnLocationChosen = UnityEngine.Random.Range(1, 6);
                    break;
                default:
                    m_spawnLocationChosen = 0; //No location found.
                    break;
            }
            if (m_spawnLocationChosen == 1)
            {
                m_spawnLocation = m_spawnPosition1;
            }
            else if (m_spawnLocationChosen == 2)
            {
                m_spawnLocation = m_spawnPosition2;
            }
            else if (m_spawnLocationChosen == 3)
            {
                m_spawnLocation = m_spawnPosition3;
            }
            else if (m_spawnLocationChosen == 4)
            {
                m_spawnLocation = m_spawnPosition4;
            }
            else if (m_spawnLocationChosen == 5)
            {
                m_spawnLocation = m_spawnPosition5;
            }
        }
        if (m_spawnLocationChosen != 0)
        {
            //Spawns Enemy
            GameObject newEnemy = Instantiate(m_enemyPrefab, m_spawnLocation, Quaternion.identity, SpawnerID);
            newEnemy.layer = 7;
            newEnemy.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Enemies");
            EnemyBehaviour enemyScript = newEnemy.GetComponent<EnemyBehaviour>();
        }
    }
}


/*
 *  GameObject projectile = Instantiate(m_projectilePrefab, m_firePoint.position, m_firePoint.rotation);
 *  projectile.layer = 8;
 *  projectile.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player");
 *   Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
 *  if (GameObject.Find(counter) == true)
 */