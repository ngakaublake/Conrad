using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMove;
    

    public Transform m_firePoint;
    public GameObject m_projectilePrefab;
    public float m_projectileForce = 20f;

    public float m_MaxPosSpread = 8;
    public float m_MaxNegSpread = -8;
    public float m_CurrentPosSpread = 0;
    public float m_CurrentNegSpread = 0;
    public float m_CurrentSpread = 0;

    public float m_TimeBetweenShots = 1.0f;
    public float m_TImeSinceLastShot = 0.0f;

    public float m_TimeBetweenSpread = 0.2f; 
    public float m_TimeSinceLastSpread = 0.0f;


    
    private void Start()
    {
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
        }
        else
        {
            if (Time.time - m_TimeSinceLastSpread >= m_TimeBetweenSpread)
            {
                DecreaseSpread();
                m_TimeSinceLastSpread = Time.time;
                
            }
           
        }

        if (Input.GetButtonDown("Fire1") && PlayerMove.m_IsPlayerAiming == true && PlayerMove.m_CurrentAmmo > 0) //Fire Weapon
        {
            Fire();
            if (Time.time - m_TImeSinceLastShot >= m_TimeBetweenShots)
            {
                //Fire();
                m_TImeSinceLastShot = Time.time;
                //PlayerMove.m_CurrentAmmo--;
            }

        }

        if (Input.GetButtonDown("Reload")) // Not working need to fix 
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.R) && PlayerMove.m_CurrentAmmo != PlayerMove.m_MaxAmmo) //Reload Gun
        {
            Reload();
        }
    }

    void Fire()
    {
        
        m_firePoint.transform.Rotate(0, 0, m_CurrentSpread, Space.Self); //Rotating the Projectile Spawn Point Angle 

        //Spawning the Projectile 
        GameObject projectile = Instantiate(m_projectilePrefab, m_firePoint.position, m_firePoint.rotation); 
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.AddForce(m_firePoint.up * m_projectileForce, ForceMode2D.Impulse); //Giving the Projectile Force 

        m_firePoint.transform.Rotate(0, 0, (-1 * m_CurrentSpread), Space.Self); //Reseting the Projectile Spawn Angle to 0 


    }
  

    void DecreaseSpread()
    {
        if (m_CurrentPosSpread != 0 && m_CurrentNegSpread != 0)
        {
            
            m_CurrentPosSpread -= 0.5f;
            m_CurrentNegSpread += 0.5f;
            m_CurrentSpread = Random.Range(m_CurrentNegSpread, m_CurrentPosSpread);
        }


        Debug.Log(m_CurrentPosSpread);
    }

    void Reload()
    {
        PlayerMove.m_CurrentAmmo = PlayerMove.m_MaxAmmo;
    }

    void GetProjectileSpread()
    {
    }
}

