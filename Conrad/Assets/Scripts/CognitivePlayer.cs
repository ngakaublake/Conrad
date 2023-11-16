using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEditor.Rendering;
using UnityEngine;

using UnityEngine.UI; //unity ui for ammo counter

public class CognitivePlayer : MonoBehaviour
{

    [SerializeField] private PlayerVision FOV;

    private PlayerController playerController;
    [SerializeField] private GameObject ConradIII;
    [SerializeField] CircleVision CircleFOV;
    public Image BloodSplatter;
    //Combat 
    public bool m_IsPlayerAiming;


    //Ammo
    public int m_RifleMaxAmmo = 5;
    public int m_ShotgunMaxAmmo = 6;
    public int m_RifleAmmoSupply = 20;
    public int m_ShotgunAmmoSupply = 0;
    public int m_RifleCurrentAmmo;
    public int m_ShotgunCurrentAmmo;

    public Text ammoCounter; //ammocounter UI

    //Health
    public int m_health;
    public int m_maxHealth = 4;
    public int m_MaxHealthKits = 4;
    public int m_CurrentHealthKits = 0;
    public float m_BloodFeedbackTimer = 0.5f;
    public float m_CurrentFeedbackTime = 0f;
    public bool m_isHealingActive = false;
    public bool m_isHoldingSpace = false;

    //Actions
    private float m_invulnerableCooldown;
    public float m_NoWarpCooldown;
    public float m_TimeToCommitAction = 2.0f;
    public float m_TimeToPerformHeal = 0.5f;
    public bool m_CurrentlyComitting = false;
    public float m_CommitActionTime = 0.0f;
    public bool m_CanWarp;
   

   

    //Rigidbody2D RB;

    //Animator ref
    public Animator animator;

    void MovePlayer() //Basic Player Movement
    {

        
        if (playerController.m_IsPlayerinCognitiveWorld == true)
        {
            FOV.SetOrigin(transform.position); //Setting the Origin Position for the Vision Cone
            CircleFOV.SetOrigin(transform.position);
        }
    }

    void AimGun()
    {
        //Makes the Vision Cone Look in current mouse Direction 
        Vector3 aimDir = playerController.transform.up;
        FOV.SetAimDirection(aimDir);

        // Checking if the right mouse button is being held down 
        if (Input.GetMouseButtonDown(1) && m_isHealingActive == false)
        {
            m_IsPlayerAiming = true;
            FOV.UpdateFOV(); //Swaps FOV between 'Explore' & 'Combat' 
            

            animator.SetBool("isAiming", true); //swap to player aim sprite

            //Adjusting player speed to be slower whilst ADS'ed 
            playerController.m_HorizontalVelocity = 0.5f;
            playerController.m_VerticalVelocity = 0.5f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerController.m_HorizontalVelocity = 0.5f;
            playerController.m_VerticalVelocity = 0.5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerController.m_HorizontalVelocity = 1f;
            playerController.m_VerticalVelocity = 1f;
        }


        if (Input.GetMouseButtonUp(1))
        {
            m_IsPlayerAiming = false;
            FOV.UpdateFOV(); //Swaps FOV between 'Explore' & 'Combat' 
            

            animator.SetBool("isAiming", false); //return to walking sprite

            //Returning Player speed to default 
            playerController.m_HorizontalVelocity = playerController.m_VelocityDefault;
            playerController.m_VerticalVelocity = playerController.m_VelocityDefault;
        }

    }

    void Start()
    {
        m_CanWarp = true;
        //RB = GetComponent<Rigidbody2D>();
        m_IsPlayerAiming = false;
        m_RifleCurrentAmmo = m_RifleMaxAmmo;
        m_ShotgunCurrentAmmo = 2;
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        m_health = 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("activate");
        }
        
        m_CurrentFeedbackTime -= Time.deltaTime;

        if (m_CurrentFeedbackTime <= 0)
        {
            BloodSplatter.enabled = false;
        }

        if (playerController != null && playerController.m_IsPlayerinCognitiveWorld == true)
        {
          
            MovePlayer();
            AimGun();
            CommitingActions();
        }

        if (playerController.m_IsPlayerMoving == true)
        {
            animator.SetFloat("animSpeed", 1);
        }
        else
        {
            animator.SetFloat("animSpeed", 0);
        }

        //Decrease Invulnerability
        if (m_invulnerableCooldown > 0.0f)
        {
            m_invulnerableCooldown -= Time.deltaTime;

            if (m_invulnerableCooldown <= 0.0f)
            {
                m_invulnerableCooldown = 0.0f; // Prevent Negative
            }
        }
        //Decrease NoWarp Cooldown
        if (m_NoWarpCooldown > 0.0f)
        {
            m_CanWarp = false;
            m_NoWarpCooldown -= 1*Time.deltaTime;

            if (m_NoWarpCooldown <= 0.0f)
            {
                UnityEngine.Debug.Log("Can Warp Again");
                m_NoWarpCooldown = 0.0f; // Prevent Negative
                m_CanWarp = true;
            }
        }
    }

    public void LoseHealth()
    {
        if (m_invulnerableCooldown == 0.0f) 
        {
            
            BloodSplatter.enabled = true;
            m_CurrentFeedbackTime = m_BloodFeedbackTimer;
            //Whatever funky effect here
            m_health = m_health - 1;
            m_NoWarpCooldown = 3.0f;
            if (m_health <= 0)
            {
                //Enter Restart mode.
                ResetCognitivePlayer();
            }
            m_invulnerableCooldown = 1.0f;
        }
    }


    void CommitingActions() //Handles all instances where the player is 'committing' to the action (Heal/Teleport)
    {
        //Heal is done with 'Space'. Priortized over Teleport.
        if (Input.GetKey(KeyCode.E) && playerController.m_CognitiveWorldResetting == false && ConradIII != null && Vector2.Distance(transform.position, ConradIII.transform.position) <= 1f)
        {
            Debug.Log("AAAAAAA");
            m_CurrentlyComitting = true;
            m_CommitActionTime += Time.deltaTime;
            if (m_CommitActionTime >= m_TimeToPerformHeal)
            {
                FOV.UpdateFOVHealing();
                m_isHealingActive = true;
                if (ConradIII != null && Vector2.Distance(transform.position, ConradIII.transform.position) <= 4f)
                {
                    // Heal
                    animator.SetTrigger("healOther");
                }
            }
        }
        else if (Input.GetKey(KeyCode.Space) && playerController.m_CognitiveWorldResetting == false && m_health < 4 && m_CurrentHealthKits > 0)
        {
            m_CurrentlyComitting = true;
            m_CommitActionTime += Time.deltaTime;
            if (m_CommitActionTime >= m_TimeToPerformHeal)
            {
                FOV.UpdateFOVHealing();
                m_isHealingActive = true;
                if (m_health != m_maxHealth && m_isHoldingSpace == false)
                {
                    m_isHoldingSpace = true;
                    m_CurrentlyComitting = false;
                    m_CommitActionTime = 0.0f;
                    m_CurrentHealthKits--;
                    Debug.Log("Current health kit " + m_CurrentHealthKits);
                    animator.SetTrigger("heal");
                }
            }
        }
        else if (Input.GetKey(KeyCode.Q) && playerController.m_CognitiveWorldResetting == false && m_NoWarpCooldown == 0)
        {
            m_CurrentlyComitting = true;
            playerController.b_beginTeleport = true;
            m_CommitActionTime += Time.deltaTime;
            if (m_CommitActionTime >= m_TimeToCommitAction)
            {
                m_CurrentlyComitting = false;
                playerController.b_beginTeleport = false;
                Debug.Log("Vwoop!");
                playerController.Teleport();
                m_CommitActionTime = 0.0f;
                FOV.DontAskMeWhatThisIs();
            }
        }
        else
        {
            //Not Commiting to an action currently.
            m_CommitActionTime = 0.0f;
            playerController.b_beginTeleport = false;
            
        }

    }
    public void HealOther()
    {
        ConradIII.GetComponent<ConradHealScript>().IncreaseHealth();
    }
    
    public void HealSelf()
    {
        Debug.Log("Heal Function" + m_CurrentHealthKits);
        m_isHealingActive = false;
        
        m_health = 4;
        FOV.ResetHealFOV();
        m_isHoldingSpace = false;
    }
    public void ResetCognitivePlayer()
    {
        //Reset Variables
        m_health = m_maxHealth;

        playerController.transform.position = playerController.m_IntialCognitiveWorldPosition;
        playerController.ResetCognitive(true);
    }

}
