using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConradHealScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CognitivePlayer cognitivePlayer;
    public int m_health;
    public int m_maxHealth;

    private float dyingRate = 15f;
    private float deathPercent = 0f;
    public float maxlifetime;
    public float minlifetime;
    private bool m_Startdying;

    // Start is called before the first frame update
    void Start()
    {
        m_health = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.m_CognitiveWorldResetting) //Respawn
        {
            m_health = m_maxHealth;
        }
        if (m_Startdying)
        {
            deathPercent += Time.deltaTime;
            if (deathPercent >= dyingRate)
            {
                m_health--;
                if (m_health <= 0)
                {
                    cognitivePlayer.ResetCognitivePlayer();
                    playerController.ResetCognitive(true);
                }
                deathPercent = 0;
                maxlifetime--;
                dyingRate = Random.Range(minlifetime, maxlifetime);
            }
        }
    }
    public void IncreaseHealth()
    {
        m_health = m_maxHealth;
    }
    public void LoseHealth()
    {
        m_health--;
    }
    public void StubToe()
    {
        m_Startdying = true;
    }
    public void Stopscript()
    {
        m_Startdying = false;
        m_health = m_maxHealth;
        deathPercent = 0;
    }
}
