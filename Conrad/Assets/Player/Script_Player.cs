using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    public float m_HorizontalVelocity = 1.5f;
    public float m_VerticalVelocity = 1.5f;
    Vector2 m_HorizontalMovement;
    Vector2 m_VerticalMovement;

    Rigidbody2D RB;

    void MovePlayer() //Basic Player Movement : (WILL CHANGE CAUSE ITS SHIT RIGHT NOW) - Patrick 
    {
        m_VerticalMovement = Vector2.zero;
        m_VerticalMovement = Vector2.zero;

        m_HorizontalMovement.x = 1.0f * Input.GetAxis("Horizontal");
        m_VerticalMovement.x = 1.0f * Input.GetAxis("Vertical");

        Vector2 Temp;
        Temp.y = m_VerticalMovement.x * m_VerticalVelocity;
        Temp.x = m_HorizontalMovement.x * m_HorizontalVelocity;

        RB.velocity = Temp;
    }

    void FollowCursor() // Aims the Player towards the Mouse Cursor
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition); //Converts Screen Pos to World Pos

        transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y)); //Makes the Character look at the Mouse

        //Printing Mouse Pos in the world to Debug Console 
        string mouseY = "Y : " + mousePos.y; 
        string mouseX = "X : " + mousePos.x;
        Debug.Log(mouseY);
        Debug.Log(mouseX);
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FollowCursor();
        MovePlayer();
        
    }


}
