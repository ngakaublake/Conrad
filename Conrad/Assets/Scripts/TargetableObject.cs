using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableObject : MonoBehaviour
{
    private Vector2 m_InitalPosition;
    public float m_initialHealth = 4.0f;
    public float m_health = 4.0f;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        m_InitalPosition = transform.position;
        m_health = m_initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.m_CognitiveWorldResetting)
        {
            //Respawn!
            m_health = m_initialHealth;
            transform.position = m_InitalPosition;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            m_health = m_health - 0.01f;
            if (m_health <= 0)
            {
                //Get Banished to the ShadowRealm, Jimbo
                transform.position = new Vector2(-60, -60);
            }
        }
    }
}
