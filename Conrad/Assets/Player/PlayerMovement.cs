using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //unity ui for ammo counter

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private PlayerVision FOV;
    //[SerializeField] private FogBehavior Fog;

    public float m_HorizontalVelocity;
    public float m_VerticalVelocity;

    public float m_VelocityDefault = 1.0f;
    Vector2 m_HorizontalMovement;
    Vector2 m_VerticalMovement;
    Vector2 m_IntialCognitiveWorldPosition; //Intial Position in Cognitive World, used to restart.
    Vector2 m_CognitiveWorldPosition; //Position in Cognitive World.
    Vector2 m_RealWorldPositon; //Position in Real World.
    public bool m_IsPlayerinCognitiveWorld;
    public bool m_IsPlayerMoving;
    public bool m_IsPlayerAiming;

    public int m_MaxAmmo = 6;
    public int m_CurrentAmmo;

    public Text ammoCounter; //ammocounter UI

    Rigidbody2D RB;

    void MovePlayer() //Basic Player Movement
    {
        if (m_IsPlayerinCognitiveWorld == true)
        {
            FOV.SetOrigin(transform.position); //Setting the Origin Position for the Vision Cone
            //Fog.m_IsRenderingCognitiveWorld = true;
        }
        else
        {
            //Fog.m_IsRenderingCognitiveWorld = false;
        }

        //Reseting the Movement Vectors
        m_VerticalMovement = Vector2.zero;
        m_VerticalMovement = Vector2.zero;
        
        //Teleport to second location
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //Check the current location of the player.
            if (m_IsPlayerinCognitiveWorld == true)
            {
                transform.position = (m_RealWorldPositon); //Move to Real World
                m_IsPlayerinCognitiveWorld = false;
                FOV.UpdateFOV();
            }
            else
            {
                transform.position = (m_CognitiveWorldPosition); //Move to Cognitive World
                m_IsPlayerinCognitiveWorld = true;
                FOV.UpdateFOV();
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
            m_RealWorldPositon = transform.position;
        }


    }

    void FollowCursor() // Aims the Player towards the Mouse Cursor
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition); //Converts Screen Pos to World Pos

        transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y)); //Makes the Character look at the Mouse

        //Makes the Vision Cone Look in current mouse Direction 
        Vector3 aimDir = transform.up;
        FOV.SetAimDirection(aimDir);

        //Printing Mouse Pos in the world to Debug Console 
        string mouseY = "Y : " + mousePos.y; 
        string mouseX = "X : " + mousePos.x;
        //Debug.Log(mouseY);
        //Debug.Log(mouseX);
    }

    void AimGun()
    {

        // Checking if the right mouse button is being held down 
        if (Input.GetMouseButtonDown(1)) 
        {
            FOV.UpdateFOV(); //Swaps FOV between 'Explore' & 'Combat' 
            m_IsPlayerAiming = true;

            //Adjusting player speed to be slower whilst ADS'ed 
            m_HorizontalVelocity = 0.5f;
            m_VerticalVelocity = 0.5f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            FOV.UpdateFOV(); //Swaps FOV between 'Explore' & 'Combat' 
            m_IsPlayerAiming = false;

            //Returning Player speed to default 
            m_HorizontalVelocity = m_VelocityDefault;
            m_VerticalVelocity = m_VelocityDefault;
        }

    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        m_IsPlayerMoving = false;
        m_IsPlayerAiming = false;
        m_CurrentAmmo = m_MaxAmmo;
        m_HorizontalVelocity = m_VelocityDefault;
        m_VerticalVelocity = m_VelocityDefault;
        m_CognitiveWorldPosition = new Vector2(0.0f, 0.0f); //Set to spawn position in Cognitive World
        m_RealWorldPositon = new Vector2(20.0f, 0.0f); //Set to spawn position in Real World.
        m_IsPlayerinCognitiveWorld = true;
    }

    void Update()
    {
        FollowCursor();
        MovePlayer();
        AimGun();

        if (m_CurrentAmmo <= 0)
            {
                ammoCounter.text = "Press 'R' to reload"; //display reload message if out of ammo
            }
        else
            {
                ammoCounter.text = m_CurrentAmmo.ToString(); //update ammo counter UI to be == currentammo var
            }
    }


}
