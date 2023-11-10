using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, EnemyDamageInterface
{
    [SerializeField] private GameObject Corpse;
    public bool b_enemyrespawns;
    public float moveSpeed = 1.0f;
    public float minimumDistance = 1.7f;
    float health = 2;
    float invulnerableCooldown = 0.0f;

    private float playerTargetZone = 4.0f;
    private float targetTargetZone = 3.0f;
    private float switchPatrolPoint = 0.0f; //How long it will take until it looks for patrolling again
    private Transform[] patrolPoints;

    private Vector2 spawnpoint;
    private float currentMinDistance = 0.5f;
    private bool PerformingAttack;
    public bool AttackDamages;
    public float AttackChargeTime;
    private float AttackTime;

    private PlayerController playerController;
    public CognitivePlayer cognitivePlayer;
    private SpriteRenderer spriteRenderer; //Resize
    public float spriteSizeMultiplier = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cognitivePlayer = FindObjectOfType<CognitivePlayer>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        spawnpoint = rb.position;

        playerController = FindObjectOfType<PlayerController>();

        AttackTime = AttackChargeTime;
        currentMinDistance = minimumDistance;
        GameObject[] patrolPointGameObjects = GameObject.FindGameObjectsWithTag("Enemy Patrol Point");
        patrolPoints = new Transform[patrolPointGameObjects.Length];

        for (int i = 0; i < patrolPointGameObjects.Length; i++)
        {
            patrolPoints[i] = patrolPointGameObjects[i].transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.m_IsPlayerinCognitiveWorld)
        {
            //Decrease Invulnerability
            if (invulnerableCooldown > 0.0f)
            {
                invulnerableCooldown -= Time.deltaTime;

                if (invulnerableCooldown <= 0.0f)
                {
                    invulnerableCooldown = 0.0f; // Prevent Negative
                }
            }

            MoveEnemy();
        }

        if (playerController.m_CognitiveWorldResetting)
        {
            if (b_enemyrespawns)
            {
                //Respawn!
                health = 2;
                transform.position = spawnpoint;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveEnemy()
    {
        //Log all instances of items the enemy could be interested in
        GameObject[] enemyTargets = GameObject.FindGameObjectsWithTag("EnemyTargets");

        //Sort by distances
        System.Array.Sort(enemyTargets, (x, y) => Vector2.Distance(transform.position, x.transform.position).CompareTo(Vector2.Distance(transform.position, y.transform.position)));

        //Patrol Point Selection
        if (switchPatrolPoint >= 0.0f)
        {
            switchPatrolPoint -= Time.deltaTime;

            if (switchPatrolPoint <= 0.0f)
            {
                SortPatrolPoints();
            }
        }

        //Attacking whilst in range
        if (PerformingAttack)
        {
            UnityEngine.Debug.Log("prep");
            AttackTime -= 1.0f * Time.deltaTime;
            if (AttackTime < 0.0f)
            {
                //Charged up, now perform attack!
                float distanceToPlayer = Vector2.Distance(transform.position, cognitivePlayer.transform.position);
                if (distanceToPlayer <= minimumDistance)
                {
                    cognitivePlayer.LoseHealth();
                    UnityEngine.Debug.Log("attackhits!");
                }
                currentMinDistance = 0.1f;
                AttackDamages = true;
                if (AttackTime < -1.0f)
                {
                    currentMinDistance = minimumDistance;
                    AttackDamages = false;
                    PerformingAttack = false;
                    AttackTime = AttackChargeTime;
                }
            }
        }


        //Target Selection
        if (Vector2.Distance(transform.position, playerController.m_CognitiveWorldPosition) <= playerTargetZone)
        {
            //Target Player
            if (Vector2.Distance(transform.position, playerController.m_CognitiveWorldPosition) > minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerController.m_CognitiveWorldPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                //Enemy Attack when in range
                PerformingAttack = true;
            }
        }
        else if (enemyTargets.Length > 0 && Vector2.Distance(transform.position, enemyTargets[0].transform.position) <= targetTargetZone)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTargets[0].transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            //Goes towards current Patrol point (patrolpoint[0]).
            if (patrolPoints.Length >= 2)
            {

                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].transform.position, moveSpeed * Time.deltaTime);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && invulnerableCooldown == 0.0f)
        {
            health = health - 1;
            if (health <= 0)
            {
                //Create the Corpse.
                Vector2 deathLocation = transform.position;
                Quaternion spawnRotation = transform.rotation;
                //Make Corpse at location (when Corpse set up)
                //Instantiate(Corpse, deathLocation, spawnRotation);

                //Die.
                Destroy(gameObject);
            }
            invulnerableCooldown = 0.0f;
        }
    }

    private void SortPatrolPoints()
    {
        if (patrolPoints.Length < 2)
        {
            return;
        }
        System.Array.Sort(patrolPoints, (x, y) => Vector2.Distance(transform.position, x.position).CompareTo(Vector2.Distance(transform.position, y.position)));

        //Set the second-nearest patrol point as the new first patrol point.
        Transform secondNearestPatrolPoint = patrolPoints[1];
        patrolPoints[1] = patrolPoints[0];
        patrolPoints[0] = secondNearestPatrolPoint;
        switchPatrolPoint = 12.0f; // Reset the timer.
    }

    public void EnemyDamage(float _dmg)
    {
        health = health - _dmg;

        if (health <= 0 )
        {
            if (b_enemyrespawns)
            {
                //Get Banished to the ShadowRealm, Jimbo
                transform.position = new Vector2(-60, -60);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
