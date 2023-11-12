using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    public Vector2 m_IntialCognitiveWorldPosition; //Intial Position in Cognitive World, used to restart.
    public Vector2 m_IntialRealWorldPosition; //Intial Position in Cognitive World, used to restart.
    public Vector2 m_CognitiveWorldPosition; //Position in Cognitive World.
    public Vector2 m_RealWorldPosition; //Position in Real World.
    public bool m_IsPlayerinCognitiveWorld;
    public bool m_IsPlayerMoving;
    public bool m_CognitiveWorldResetting = false;
    public float m_CognitiveWorldResetCooldown = 1.0f;
    public bool b_ForceTeleport = false;
    public bool b_beginTeleport;
    private bool b_currentlyTeleporting;
    private bool b_canmove;
    public SpriteRenderer s_overlaysprite;


    //Main Key things
    public bool m_key1Obtained;
    public bool m_key2Obtained;
    public bool m_key3Obtained;
    public bool m_key4Obtained;


    private Transform dog;
        Rigidbody2D RB;

    void MovePlayer() //Basic Player Movement
    {
        //Reseting the Movement Vectors
        m_VerticalMovement = Vector2.zero;
        m_HorizontalMovement = Vector2.zero;

        //Getting the Direction Input 
        m_HorizontalMovement.x = 1.0f * Input.GetAxis("Horizontal");
        m_VerticalMovement.x = 1.0f * Input.GetAxis("Vertical");

        Vector2 Temp;
        Temp.y = m_VerticalMovement.x * m_VerticalVelocity;
        Temp.x = m_HorizontalMovement.x * m_HorizontalVelocity;

        //Apply the Velocity to the Player (RB)
        RB.velocity = Temp;

        if (Input.GetKeyDown(KeyCode.Q) && m_IsPlayerinCognitiveWorld == false)
        {
            float distanceToDoggo = Vector2.Distance(transform.position, dog.position);
            if (distanceToDoggo <= 0.8f && !b_beginTeleport)
            {
                b_beginTeleport = true;
                StartCoroutine(TeleportAfterDelay(3f));
            }
        }

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

    IEnumerator TeleportAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Teleport();
    }

    public void Teleport()
    {
        b_beginTeleport = false;
        //Check the current location of the player.
        if (m_IsPlayerinCognitiveWorld == true)
        {
            //Disable the current player in the Cognitive World and enable the player in the Real World
            CognitivePlayer.SetActive(false);
            RealPlayer.SetActive(true);

            m_IsPlayerinCognitiveWorld = false;
            transform.position = (m_RealWorldPosition);
        }
       else if (m_IsPlayerinCognitiveWorld == false)
        {
            // Disable the current player in the Real World and enable the player in the Cognitive World
            RealPlayer.SetActive(false);
            CognitivePlayer.SetActive(true);

            m_IsPlayerinCognitiveWorld = true;

            m_IsPlayerinCognitiveWorld = true;
            transform.position = (m_CognitiveWorldPosition); //Move to Cognitive World
        }
    }

    public void ScriptedTeleport(bool b_ForceTeleport)
    {
        //Reset only happens if confirmed true
        if (b_ForceTeleport)
        {
            Teleport();
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
        dog = GameObject.FindGameObjectWithTag("Good Boy").transform;
        m_IsPlayerMoving = false;
        m_HorizontalVelocity = m_VelocityDefault;
        m_VerticalVelocity = m_VelocityDefault;
        m_CognitiveWorldPosition = m_IntialCognitiveWorldPosition; //Set to spawn position in Cognitive World
        m_RealWorldPosition = m_IntialRealWorldPosition; //Set to spawn position in Real World.
        m_IsPlayerinCognitiveWorld = false;
        CognitivePlayer.SetActive(false);
        transform.position = m_RealWorldPosition;
        b_canmove = true;
        CollectKey(0); //Set Keys to 0
    }

    void Update()
    {
        if (!m_CognitiveWorldResetting)
        {
            FollowCursor();
            if (b_canmove)
            {
                MovePlayer();
            }
        }
        ResetCognitive(false); //Allows updating when reset is true
    }


    public void CollectKey(int key)
    {
        switch (key)
        {
            case 1:
                m_key1Obtained = true;
                break;
                case 2:
                m_key2Obtained = true;
                break;
            case 3:
                m_key3Obtained = true;
                break;
                case 4:
                m_key4Obtained = true;
                break;
            default:
                m_key1Obtained = false;
                m_key2Obtained = false;
                m_key3Obtained = false;
                m_key4Obtained = false;
            break;
        }
    }


    public void ResetCognitive(bool resetConfirmation)
    {
        //Reset only happens if confirmed true
        if (resetConfirmation)
        {
            m_CognitiveWorldResetting = resetConfirmation;
            m_RealWorldPosition = m_IntialRealWorldPosition;
            m_CognitiveWorldPosition = m_IntialCognitiveWorldPosition;
            ScriptedTeleport(true); //Move to real world
        }
        if (m_CognitiveWorldResetting)
        {
            m_CognitiveWorldResetCooldown -= Time.deltaTime* 2;
            if (m_CognitiveWorldResetCooldown <= 0.0f)
            {
                m_CognitiveWorldResetting = false;
              m_CognitiveWorldResetCooldown = 1.0f; // Reset
            }
        }

        //Overlay for Teleporting
        if (b_beginTeleport && !b_currentlyTeleporting)
        {
            StartCoroutine(TeleportVisuals(3f));
        }
        else if (!b_beginTeleport)
        {
            StopCoroutine("TeleportVisuals");
            Color transparentColour = new Color(s_overlaysprite.color.r, s_overlaysprite.color.g, s_overlaysprite.color.b, 0f);
            s_overlaysprite.color = transparentColour;
            b_currentlyTeleporting = false;
        }

        IEnumerator TeleportVisuals(float TeleportTime)
        {
            b_currentlyTeleporting = true;
            float elapsedTime = 0f;
            Color transparentColour = new Color(s_overlaysprite.color.r, s_overlaysprite.color.g, s_overlaysprite.color.b, 0f);
            Color VisibleColour = new Color(transparentColour.r, transparentColour.g, transparentColour.b, 0.8f); //Opaque
            yield return new WaitForSeconds(1f);
            while (elapsedTime < TeleportTime && b_beginTeleport)
            {
                //Colour
                s_overlaysprite.color = Color.Lerp(transparentColour, VisibleColour, elapsedTime / TeleportTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            s_overlaysprite.color = VisibleColour; // Ensure it reaches the fully opaque state
            s_overlaysprite.color = transparentColour;
        }
    }



    public void StopMoving(int _time)
    {
        if (b_canmove)
        {
            b_canmove = false;
            RB.velocity = new Vector2 (0,0);
            m_IsPlayerMoving = false;

            StartCoroutine(StartMoving(_time));
        }
    }
    private IEnumerator StartMoving(int _delay)
    {
        yield return new WaitForSeconds(_delay);
        b_canmove = true;
    }

}
