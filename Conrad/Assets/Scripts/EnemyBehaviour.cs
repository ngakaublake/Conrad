using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, EnemyDamageInterface
{
    [SerializeField] private GameObject Corpse;
    public bool b_enemyrespawns;
    public float moveSpeed = 1.2f;
    public float minimumDistance = 1.7f;
    float health = 2;
    float invulnerableCooldown = 0.0f;

    private float playerTargetZone = 4.0f;
    private float targetTargetZone = 12.0f;
    private float switchPatrolPoint = 0.0f; //How long it will take until it looks for patrolling again
    private Transform[] patrolPoints;

    private Vector2 spawnpoint;
    private bool PerformingAttack;
    public bool AttackDamages;
    public float AttackChargeTime;
    private float AttackTime;
    public PlayerController playerController;
    public CognitivePlayer cognitivePlayer;
    private SpriteRenderer spriteRenderer; //Resize
    public float spriteSizeMultiplier = 2.0f;
    [SerializeField] ParticleSystem BloodSpurt;
    public Animator animator;
    [SerializeField] AudioClip meleeSound;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (cognitivePlayer == null )
        {
            cognitivePlayer = FindObjectOfType<CognitivePlayer>();
        }

        spawnpoint = rb.position;

        playerController = FindObjectOfType<PlayerController>();
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

        //cognitivePlayer = FindObjectOfType<CognitivePlayer>();
        if (playerController != null)
        {
            //Look at Player
            Vector2 direction = playerController.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 270);
        }


        if (playerController.m_IsPlayerinCognitiveWorld)
        {
            //Decrease Cooldown
            if (AttackChargeTime > 0.0f)
            {
                AttackChargeTime -= Time.deltaTime;

                if (AttackChargeTime <= 0.0f)
                {
                    AttackChargeTime = 0.0f; // Prevent Negative
                }
            }

            MoveEnemy();
        }

        if (playerController.m_CognitiveWorldResetting)
        {
            if (playerController.b_EndingCutsceneTriggered)
            {
                BloodSpurt.Play();
                //Create the Corpse.
                Vector2 deathLocation = transform.position;
                Quaternion spawnRotation = transform.rotation;
                //Make Corpse at location (when Corpse set up)
                Instantiate(Corpse, deathLocation, spawnRotation);
            }
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
                //performingAttack = true;
                if (AttackChargeTime == 0f)
                {
                    animator.SetTrigger("attack");
                }
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

    void Attack() //function called on enemy strike frame
    {
        float distanceToPlayer = Vector2.Distance(transform.position, cognitivePlayer.transform.position);
        if (distanceToPlayer <= minimumDistance)
        {
            cognitivePlayer.LoseHealth();
        }
        AttackChargeTime = 3f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && invulnerableCooldown == 0.0f)
        {
            audioSource.PlayOneShot(meleeSound);
            BloodSpurt.Play();

            health = health - 1;
            if (health <= 0)
            {
                BloodSpurt.Play();
                //Create the Corpse.
                Vector2 deathLocation = transform.position;
                Quaternion spawnRotation = transform.rotation;
                //Make Corpse at location (when Corpse set up)
                Instantiate(Corpse, deathLocation, spawnRotation);

                //Die.
                Destroy(gameObject);
            }
            invulnerableCooldown = 0.0f;
        }
    }

    public void EnemyDamage(float _dmg)
    {
        audioSource.PlayOneShot(meleeSound);
        BloodSpurt.Play();

        health = health - _dmg;
        if (health <= 0 )
        {
            BloodSpurt.Play();
            //Create the Corpse.
            Vector2 deathLocation = transform.position;
            Quaternion spawnRotation = transform.rotation;
            //Make Corpse at location (when Corpse set up)
            Instantiate(Corpse, deathLocation, spawnRotation);
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
