using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float minimumDistance = 0.9f;
    int health = 2;
    float invulnerableCooldown = 0.0f;

    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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

            if (Vector2.Distance(transform.position, playerController.m_CognitiveWorldPosition) > minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerController.m_CognitiveWorldPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                //Enemy Attack when in range
            }
        }

        if (playerController.m_CognitiveWorldResetting)
        {
            //Dies.
            Destroy(gameObject);
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
                //Instantiate(yourCorpsePrefab, deathLocation, spawnRotation);

                //Die.
                Destroy(gameObject);
            }
            invulnerableCooldown = 3.0f;
        }
    }
}
