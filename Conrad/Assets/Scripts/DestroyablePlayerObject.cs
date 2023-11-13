using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlayerObject : MonoBehaviour
{
    public Sprite[] sprites; //An Array! @ Ray Emata
    private SpriteRenderer spriteRenderer;
    private Vector2 m_InitalPosition;
    public int m_initialHealth = 4;
    public int m_health = 4;
    private PlayerController playerController;
    public int currentSpriteIndex = 0;

    public string ChildrenPrefix; //Input Child Prefix DONT PUT THE NUMBER

    // Start is called before the first frame update
    void Start()
    {

        playerController = FindObjectOfType<PlayerController>();
        m_InitalPosition = transform.position;
        m_health = m_initialHealth;


        for (int i = 1; i < m_initialHealth + 1; i++)
        {
            string counter = ChildrenPrefix + i;
            if (GameObject.Find(counter) == true)
            {
                GameObject CurrentWall = GameObject.Find(counter);
                CurrentWall.GetComponent<PolygonCollider2D>().enabled = false;
                CurrentWall.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 1; i < m_initialHealth + 1; i++)
        {

            string counter = ChildrenPrefix + "" + i;
            if (m_health == i)
            {
                if (GameObject.Find(counter) == true)
                {
                    GameObject CurrentWall = GameObject.Find(counter);

                    CurrentWall.GetComponent<PolygonCollider2D>().enabled = true;
                    CurrentWall.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                if (GameObject.Find(counter) == true)
                {
                    GameObject OldWall = GameObject.Find(counter);
                    OldWall.GetComponent<PolygonCollider2D>().enabled = false;
                    OldWall.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

    }


    public void GetHit()
    {
        m_health = m_health - 1;
        if (m_health <= 0)
        {

            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

}