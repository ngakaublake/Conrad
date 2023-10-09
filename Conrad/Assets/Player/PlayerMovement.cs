using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private PlayerVision FOV; 

    public float m_HorizontalVelocity = 1.5f;
    public float m_VerticalVelocity = 1.5f;
    Vector2 m_HorizontalMovement;
    Vector2 m_VerticalMovement;
    public bool m_IsPlayerMoving;
    public bool m_IsPlayerAiming;

    public int m_MaxAmmo = 6;
    public int m_CurrentAmmo;

    Rigidbody2D RB;

    void MovePlayer() //Basic Player Movement
    {
        FOV.SetOrigin(transform.position); //Setting the Origin Position for the Vision Cone

        //Reseting the Movement Vectors
        m_VerticalMovement = Vector2.zero;
        m_VerticalMovement = Vector2.zero;
        
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
            m_HorizontalVelocity = 1.5f;
            m_VerticalVelocity = 1.5f;
        }

    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        m_IsPlayerMoving = false;
        m_IsPlayerAiming = false;
        m_CurrentAmmo = m_MaxAmmo;
    }

    void Update()
    {
        FollowCursor();
        MovePlayer();
        AimGun();
    }


}
