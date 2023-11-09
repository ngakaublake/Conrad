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
    private GameObject ConradIII;

    //Combat 
    public bool m_IsPlayerAiming;


    //Ammo
    public int m_RifleMaxAmmo = 5;
    public int m_ShotgunMaxAmmo = 6;
    public int m_RifleAmmoSupply = 30;
    public int m_ShotgunAmmoSupply = 69;
    public int m_RifleCurrentAmmo;
    public int m_ShotgunCurrentAmmo;

    public Text ammoCounter; //ammocounter UI

    //Health
    public int m_health;
    public int m_maxHealth = 4;
    public int m_MaxHealthKits = 4;
    public int m_CurrentHealthKits = 0;

   

    //Actions
    private float m_invulnerableCooldown;
    public float m_TimeToCommitAction = 2.0f;
    public bool m_CurrentlyComitting = false;
    public float m_CommitActionTime = 0.0f;
   

    //Rigidbody2D RB;

    //Animator ref
    public Animator animator;

    void MovePlayer() //Basic Player Movement
    {

        
        if (playerController.m_IsPlayerinCognitiveWorld == true)
        {
            FOV.SetOrigin(transform.position); //Setting the Origin Position for the Vision Cone

        }
    }

    void AimGun()
    {
        //Makes the Vision Cone Look in current mouse Direction 
        Vector3 aimDir = playerController.transform.up;
        FOV.SetAimDirection(aimDir);

        // Checking if the right mouse button is being held down 
        if (Input.GetMouseButtonDown(1))
        {
            FOV.UpdateFOV(); //Swaps FOV between 'Explore' & 'Combat' 
            m_IsPlayerAiming = true;

            animator.SetBool("isAiming", true); //swap to player aim sprite

            //Adjusting player speed to be slower whilst ADS'ed 
            playerController.m_HorizontalVelocity = 0.5f;
            playerController.m_VerticalVelocity = 0.5f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            FOV.UpdateFOV(); //Swaps FOV between 'Explore' & 'Combat' 
            m_IsPlayerAiming = false;

            animator.SetBool("isAiming", false); //return to walking sprite

            //Returning Player speed to default 
            playerController.m_HorizontalVelocity = playerController.m_VelocityDefault;
            playerController.m_VerticalVelocity = playerController.m_VelocityDefault;
        }

    }

    void Start()
    {
        //RB = GetComponent<Rigidbody2D>();
        m_IsPlayerAiming = false;
        m_RifleCurrentAmmo = m_RifleMaxAmmo;
        m_ShotgunCurrentAmmo = m_ShotgunMaxAmmo;
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        m_health = m_maxHealth;
        ConradIII = GameObject.FindGameObjectWithTag("ConradIII"); //Makes sure I know where Conrad is for Act III
    }

    void Update()
    {


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
    }

    public void LoseHealth()
    {
        if (m_invulnerableCooldown == 0.0f) 
        {
            //Whatever funky effect here
            m_health = m_health - 1;
            if (m_health <= 0)
            {
                //Enter Restart mode.
                ResetCognitivePlayer();
            }
            m_invulnerableCooldown = 2.0f;
        }
    }


    void CommitingActions() //Handles all instances where the player is 'committing' to the action (Heal/Teleport)
    {
        //Heal is done with 'Space'. Priortized over Teleport.
        if (Input.GetKey(KeyCode.Space) && playerController.m_CognitiveWorldResetting == false && m_health < 4 && m_CurrentHealthKits > 0)
        {
            m_CurrentlyComitting = true;
            m_CommitActionTime += Time.deltaTime;
            if (m_CommitActionTime >= m_TimeToCommitAction)
            {
                if (ConradIII != null && Vector2.Distance(transform.position, ConradIII.transform.position) <= 4f)
                {
                    // Heal
                    ConradIII.GetComponent<ConradHealScript>().IncreaseHealth();
                }
                else
                {
                    m_CurrentlyComitting = false;
                    m_health = 4;
                    m_CurrentHealthKits--;
                    m_CommitActionTime = 0.0f;
                }
            }
        }
        else if (Input.GetKey(KeyCode.Q) && playerController.m_CognitiveWorldResetting == false)
        {
            m_CurrentlyComitting = true;
            m_CommitActionTime += Time.deltaTime;
            if (m_CommitActionTime >= m_TimeToCommitAction)
            {
                m_CurrentlyComitting = false;
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
        }

    }


    private void ResetCognitivePlayer()
    {
        //Reset Variables
        m_health = m_maxHealth;

        playerController.transform.position = playerController.m_IntialCognitiveWorldPosition;
        playerController.ResetCognitive(true);
    }

}
