using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI; //unity ui for ammo counter

public class PlayerController : MonoBehaviour
{
    public GameObject RealPlayer;
    public GameObject CognitivePlayer;

    public float m_HorizontalVelocity;
    public float m_VerticalVelocity;

    public float m_VelocityDefault = 1.0f;
    Vector2 m_HorizontalMovement;
    Vector2 m_VerticalMovement;
    Vector2 m_IntialCognitiveWorldPosition; //Intial Position in Cognitive World, used to restart.
    Vector2 m_CognitiveWorldPosition; //Position in Cognitive World.
    Vector2 m_RealWorldPosition; //Position in Real World.
    public bool m_IsPlayerinCognitiveWorld;
    public bool m_IsPlayerMoving;

    //public Animator animator;

    Rigidbody2D RB;

    void MovePlayer() //Basic Player Movement
    {
        //Reseting the Movement Vectors
        m_VerticalMovement = Vector2.zero;
        m_HorizontalMovement = Vector2.zero;

        //Teleport to second location
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Check the current location of the player.
            if (m_IsPlayerinCognitiveWorld == true)
            {
                // Disable the current player in the Cognitive World and enable the player in the Real World
                CognitivePlayer.SetActive(false);
                RealPlayer.SetActive(true);

                m_IsPlayerinCognitiveWorld = false;
                transform.position = (m_RealWorldPosition);
            }
            else
            {
                // Disable the current player in the Real World and enable the player in the Cognitive World
                RealPlayer.SetActive(false);
                CognitivePlayer.SetActive(true);

                m_IsPlayerinCognitiveWorld = true;

                m_IsPlayerinCognitiveWorld = true;
                transform.position = (m_CognitiveWorldPosition); //Move to Cognitive World
            }
        }


        //Getting the Direction Input 
        m_HorizontalMovement.x = 1.0f * Input.GetAxis("Horizontal");
        m_VerticalMovement.x = 1.0f * Input.GetAxis("Vertical");

        Vector2 Temp;
        Temp.y = m_VerticalMovement.x * m_VerticalVelocity;
        Temp.x = m_HorizontalMovement.x * m_HorizontalVelocity;

        //Apply the Velocity to the Player (RB)
        RB.velocity = Temp;



        //Checking if the Player is Moving
        if (Temp.x == 0.0f && Temp.y == 0.0f)
        {
            m_IsPlayerMoving = false;
        }
        else
        {
            m_IsPlayerMoving = true;
        }

        //Update position in world that Player is in
        if (m_IsPlayerinCognitiveWorld == true)
        {
            //Updates Position to current position.
            //Should remember this position when teleporting. 
            m_CognitiveWorldPosition = transform.position;
        }
        else if (m_IsPlayerinCognitiveWorld == false)
        {
            m_RealWorldPosition = transform.position;
        }


    }

    void FollowCursor() // Aims the Player towards the Mouse Cursor
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition); //Converts Screen Pos to World Pos

        transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y)); //Makes the Character look at the Mouse

        //Printing Mouse Pos in the world to Debug Console 
        string mouseY = "Y : " + mousePos.y;
        string mouseX = "X : " + mousePos.x;
        //Debug.Log(mouseY);
        //Debug.Log(mouseX);
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        m_IsPlayerMoving = false;
        m_HorizontalVelocity = m_VelocityDefault;
        m_VerticalVelocity = m_VelocityDefault;
        m_CognitiveWorldPosition = new Vector2(0.0f, 0.0f); //Set to spawn position in Cognitive World
        m_RealWorldPosition = new Vector2(20.0f, 0.0f); //Set to spawn position in Real World.
        m_IsPlayerinCognitiveWorld = false;
        CognitivePlayer.SetActive(false);
        transform.position = m_RealWorldPosition;
    }

    void Update()
    {
        FollowCursor();
        MovePlayer();

        //if (m_IsPlayerinCognitiveWorld == true)
        //{
        //    animator.SetBool("isCognitive", true);
        //}
        //else
        //{
        //    animator.SetBool("isCognitive", false);
        //}
    }

}
