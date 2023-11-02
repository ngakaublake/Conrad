using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    public float m_walkSpeed = 0.2f;
    public float m_wanderRadius = 1.0f;
    public float m_followRadius = 1.0f;
    private float m_timeUntilBored = 0.0f;
    private float m_boredThreshold = 4.0f;
    private float m_minimumDistance = 0.5f;
    private bool m_boredWandering = false;

    private Transform player;
    private Vector2 targetPosition;
    private bool m_followingPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetNewRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_boredWandering == false)
        {
            CheckIfPlayerNearby();
        }

        if (m_followingPlayer)
        {

            //Dog will follow you!
            if (Vector2.Distance(transform.position, player.position) > m_minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, m_walkSpeed * Time.deltaTime);
            }
            else
            {
                //Dog has gotten within range of player. Will begin getting bored.
                m_timeUntilBored += Time.deltaTime;
                if (m_timeUntilBored >= m_boredThreshold)
                {
                    //Got bored. Will wander elsewhere.
                    m_boredWandering = true;
                    m_timeUntilBored = Random.Range(5.0f, 11.0f);
                    m_followingPlayer = false;
                }
            }
        }
        else
        {
            // Wander randomly
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                GetNewRandomDestination();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, m_walkSpeed * Time.deltaTime);
            }
        }

        ProcessBoredom();
    }

    void ProcessBoredom()
    {
        if (m_boredWandering && m_timeUntilBored > 0)
        {
            m_timeUntilBored -= Time.deltaTime;
            if (m_timeUntilBored <= 0)
            {
                m_timeUntilBored = 0;
                m_boredWandering = false;
            }
        }
    }

    void CheckIfPlayerNearby()
    {
        if (Vector2.Distance(transform.position, player.position) < m_followRadius)
        {
            m_followingPlayer = true;
        }
        else
        {
            m_followingPlayer = false;
        }
    }

    void GetNewRandomDestination()
    {
        targetPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * m_wanderRadius;
    }
}