using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

using UnityEngine.UI; //unity ui for ammo counter

public class CognitivePlayer : MonoBehaviour
{

    [SerializeField] private PlayerVision FOV;

    private PlayerController playerController;


    public bool m_IsPlayerAiming;

    public int m_RifleMaxAmmo = 5;
    public int m_ShotgunMaxAmmo = 6;
    public int m_RifleAmmoSupply = 30;
    public int m_ShotgunAmmoSupply = 69;

    public int m_RifleCurrentAmmo;
    public int m_ShotgunCurrentAmmo;

    public Text ammoCounter; //ammocounter UI

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
    }

    void Update()
    {
        if (playerController != null && playerController.m_IsPlayerinCognitiveWorld == true)
        {
            MovePlayer();
            AimGun();
        }

        if (playerController.m_IsPlayerMoving == true)
        {
            //Debug.Log("moving");
            animator.SetFloat("animSpeed", 1);
        }
        else
        {
            //Debug.Log("standin");
            animator.SetFloat("animSpeed", 0);
        }

    }


}
