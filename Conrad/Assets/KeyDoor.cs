using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private PlayerController m_PlayerControllerRef;

    private bool isInRange; //Checks if player is in range of pickup

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "PlayerReach")
        {
            Debug.Log("IN RADIUS");
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerReach")
        {
            isInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange == true && Input.GetKeyDown(KeyCode.E))
        {
            if (m_PlayerControllerRef.m_key1Obtained && m_PlayerControllerRef.m_key2Obtained & m_PlayerControllerRef.m_key3Obtained && m_PlayerControllerRef.m_key4Obtained)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
          
            
    }
}
