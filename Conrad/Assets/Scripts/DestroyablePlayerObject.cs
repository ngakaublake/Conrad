using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlayerObject : MonoBehaviour
{
    public Sprite[] sprites; //An Array!
    private SpriteRenderer spriteRenderer;
    private Vector2 m_InitalPosition;
    public int m_initialHealth = 6;
    public int m_health = 6;
    private PlayerController playerController;
    public int currentSpriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        m_InitalPosition = transform.position;
        m_health = m_initialHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }

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
        spriteRenderer.sprite = sprites[m_health];
    }

    public void GetHit()
    {

        UnityEngine.Debug.Log("HIT");
        m_health = m_health - 1;
        if (m_health <= 0)
        {
        //Get Banished to the ShadowRealm, Jimbo
         transform.position = new Vector2(-60, -60);
        }
    }
}
