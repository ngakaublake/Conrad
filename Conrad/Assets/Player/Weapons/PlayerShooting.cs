using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //Refrences
    [SerializeField] private PlayerMovement PlayerMove;
    [SerializeField] private PlayerCrosshair Crosshair; 

    //Transform for FirePoint World Object 
    public Transform m_firePoint;

    //Bullet Objects
    public GameObject m_projectilePrefab;
    public GameObject m_MaskPrefab;

    public float m_projectileForce = 20f; //Bullet Velocity 

    //muzzle flash object
    public GameObject m_muzzleflash;

    //Varibles for Weapon Bloom
    public float m_MaxPosSpread = 8;
    public float m_MaxNegSpread = -8;
    public float m_CurrentPosSpread = 0;
    public float m_CurrentNegSpread = 0;
    public float m_CurrentSpread = 0;

    //Timing Varibles
    public float m_TimeBetweenShots = 0.6f;
    public float m_TImeSinceLastShot = 0.0f;

    public float m_TimeBetweenSpread = 0.2f; 
    public float m_TimeSinceLastSpread = 0.0f;

    //Animator ref
    public Animator animator;

    private void Start()
    {
        //Setting Default Values on Start up 
        m_CurrentPosSpread = m_MaxPosSpread;
        m_CurrentNegSpread = m_MaxNegSpread;
        m_CurrentSpread = m_MaxPosSpread;
    }

    void Update()
    {

        //Weapon Bloom 
        if (PlayerMove.m_IsPlayerMoving == true) //This is gonna change to increase slowly by moving but for just gonna fully reset the spread 
        {
            m_CurrentSpread = Random.Range(m_MaxNegSpread, m_MaxPosSpread); //Range of Spread in Degrees 
            m_CurrentPosSpread = m_MaxPosSpread;
            m_CurrentNegSpread = m_MaxNegSpread;
            Crosshair.ResetScaleCrosshair();
        }
        else //Player isnt moving 
        {
            if (Time.time - m_TimeSinceLastSpread >= m_TimeBetweenSpread) //Tighten Weapon Bloom 
            {
                DecreaseSpread();
                m_TimeSinceLastSpread = Time.time;
                
            }
           
        }

        if (Input.GetButtonDown("Fire1") && PlayerMove.m_IsPlayerAiming == true && PlayerMove.m_CurrentAmmo > 0) //Fire Weapon
        {
            //Fire();
            if (Time.time - m_TImeSinceLastShot >= m_TimeBetweenShots)
            {
                Fire(); //Firing the Projectile 
                m_TImeSinceLastShot = Time.time; //Reseting the Time since last shot 
                PlayerMove.m_CurrentAmmo--; //Adjusting Ammo Count 


                
            }

        }

        if (Input.GetButtonDown("Reload")) // Not working need to fix 
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.R) && PlayerMove.m_CurrentAmmo != PlayerMove.m_MaxAmmo) //Reload
        {
            Reload();
        }
    }

    void Fire() //Fires the Weapon 
    {
        animator.SetTrigger("hasShot"); //placeholder. Sets animation param to activate fire animation

        m_firePoint.transform.Rotate(0, 0, m_CurrentSpread, Space.Self); //Rotating the Projectile Spawn Point Angle 

        //spawning the muzzleflash
        //GameObject muzzleFlash = Instantiate(m_muzzleflash, m_firePoint.position, m_firePoint.rotation); //currently non-functional. spawns at 90 degree angle and does not despawn

        //Spawning the Projectile 
        GameObject projectile = Instantiate(m_projectilePrefab, m_firePoint.position, m_firePoint.rotation);
        GameObject ProjectileMask = Instantiate(m_MaskPrefab, m_firePoint.position, m_firePoint.rotation);
       
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Rigidbody2D rbMask = ProjectileMask.GetComponent<Rigidbody2D>();

        //Setting the Layer
        projectile.layer = 8;
        ProjectileMask.layer = 8;

        projectile.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player");
        ProjectileMask.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player");

        //Giving the Objects Force / Velocity
        rb.AddForce(m_firePoint.up * m_projectileForce, ForceMode2D.Impulse); 
        rbMask.AddForce(m_firePoint.up * m_projectileForce, ForceMode2D.Impulse); 

        m_firePoint.transform.Rotate(0, 0, (-1 * m_CurrentSpread), Space.Self); //Reseting the Projectile Spawn Angle to 0 


    }
  

    void DecreaseSpread() //Deecrease the Weapon Bloom
    {
        if (m_CurrentPosSpread != 0 && m_CurrentNegSpread != 0)
        {
            
            m_CurrentPosSpread -= 0.5f;
            m_CurrentNegSpread += 0.5f;
            m_CurrentSpread = Random.Range(m_CurrentNegSpread, m_CurrentPosSpread);
            Crosshair.ScaleCrosshair();
        }


        Debug.Log(m_CurrentPosSpread);
    }

    void Reload() //Reloads the Gun 
    {
        PlayerMove.m_CurrentAmmo = PlayerMove.m_MaxAmmo;
    }

    void GetProjectileSpread()
    {
    }

}

