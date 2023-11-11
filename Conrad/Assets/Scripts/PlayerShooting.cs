using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon 
{ 
    Weapon_Shotgun,
    Weapon_Rifle,
    Weapon_Melee,
    Weapon_Mg,
    Weapon_Fist
};


public class PlayerShooting : MonoBehaviour
{
    //Refrences
    [SerializeField] private CognitivePlayer PlayerMove;
    [SerializeField] private PlayerCrosshair Crosshair;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AmmoCounter weaponUI;

    //Transforms for FirePoint & MeleePoint World Object 
    public Transform m_firePoint;
    public Transform m_MeleePoint;
    

    private RaycastHit2D[] m_MeleeHits;
    [SerializeField] private Transform meeleTransform;
    [SerializeField] private float meleeRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;

    public float m_MeleeAttackRange = 1.5f;

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
    public float m_ShotgunTimeBetweenShots = 1.2f;
    public float m_ShotgunTimeSinceLastShot = 0.0f;

    //Timing Varibles
    public float m_RifleTimeBetweenShots = 1.0f;
    public float m_RifleTimeSinceLastShot = 0.0f;

    public float m_TimeBetweenSpread = 0.2f;
    public float m_TimeSinceLastSpread = 0.0f;

    //Melee Timers
    public float m_TimeSinceMeleeHeld = 0.0f;
    public float m_TimeMaxHoldMelee = 1.5f;
    public bool m_IsHoldingMelee = false;

    //Animator ref
    public Animator animator;

    //Enum for Weapon
    public Weapon CurrentWeapon;

    private void Start()
    {
        //Setting Default Values on Start up 
        m_CurrentPosSpread = m_MaxPosSpread;
        m_CurrentNegSpread = m_MaxNegSpread;
        m_CurrentSpread = m_MaxPosSpread;
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        //CurrentWeapon = Weapon.Weapon_Shotgun;
        CurrentWeapon = Weapon.Weapon_Rifle;
    }

    void Update()
    {
        animator.SetInteger("ammoSupply", PlayerMove.m_ShotgunAmmoSupply);
        animator.SetInteger("shellCount", PlayerMove.m_ShotgunCurrentAmmo);
        WeaponCheck(); //What weapon is being used for animations)

        WeaponBloom();

        if (Input.GetButtonDown("Fire1") && PlayerMove.m_IsPlayerAiming == true) //Fire Weapon
        {
            Fire();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Swap To Rifle
        {
            CurrentWeapon =  Weapon.Weapon_Rifle;
            weaponUI.DisableShotgunCount();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) //Swap to Shotgun
        {
            CurrentWeapon = Weapon.Weapon_Shotgun;
            weaponUI.DisableRifleCount();
        }

        if (Input.GetKey(KeyCode.LeftShift)) //melee keybind and animations
        {
            animator.SetBool("isMelee", true);
            m_TimeSinceMeleeHeld = m_TimeSinceMeleeHeld + 1.0f * Time.deltaTime;
           
            if (m_TimeSinceMeleeHeld >= m_TimeMaxHoldMelee)
            {
                Debug.Log("MAX DMG");
            }
        }
        else
        {

            animator.SetBool("isMelee", false);
           
        }

        if (Input.GetKeyDown(KeyCode.R)) //Reload && PlayerMove.m_CurrentAmmo != PlayerMove.m_MaxAmmo
        {
            animator.SetTrigger("reload"); //set shared player animation trigger to reload
        }
    }

    void Fire() //Fires the Weapon 
    {
        switch (CurrentWeapon)
        {
            case Weapon.Weapon_Rifle:
                
                if (Time.time - m_RifleTimeSinceLastShot >= m_RifleTimeBetweenShots && PlayerMove.m_RifleCurrentAmmo > 0)
                {
                    FireRifle();
                    m_RifleTimeSinceLastShot = Time.time; //Reseting the Time since last shot 
                    PlayerMove.m_RifleCurrentAmmo--; //Adjusting Ammo Count 
                }
                break;

            case Weapon.Weapon_Shotgun:

                if (Time.time - m_ShotgunTimeSinceLastShot >= m_ShotgunTimeBetweenShots && PlayerMove.m_ShotgunCurrentAmmo > 0)
                {
                    FireShotgun();
                    m_ShotgunTimeSinceLastShot = Time.time; //Reseting the Time since last shot 
                    PlayerMove.m_ShotgunCurrentAmmo--; //Adjusting Ammo Count 
                    animator.SetInteger("shellCount", PlayerMove.m_ShotgunCurrentAmmo);
                }            
                break;

            case Weapon.Weapon_Melee:
                //MeleeAttack();
                break;

            default:
                Debug.Log("no weapon selected");
                break;

        }

    }

    void WeaponCheck() //Checks what weapon is currently equipped to ensure correct sprite is shown
    {
        switch (CurrentWeapon)
        {
            case Weapon.Weapon_Rifle:
                animator.SetBool("hasRifle", true);
                break;

            case Weapon.Weapon_Shotgun:
                animator.SetBool("hasRifle", false);
                break;
        }
    }

    void FireRifle()
    {
        animator.SetTrigger("hasShot"); //Sets animation param to activate fire animation

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

        //Player cannot teleport whilst in this state
        PlayerMove.m_NoWarpCooldown = 10.0f;

    }

    void FireShotgun()
    {
        animator.SetTrigger("hasShot"); //Sets animation param to activate fire animation
        
        for (int i = 0; i != 4; i++)
        {
            float ShotGunSpread = 0.0f;
            if (m_CurrentPosSpread > 2.0f && m_CurrentNegSpread < -2.0f)
            {
                ShotGunSpread = Random.Range(m_CurrentNegSpread - 2.0f, m_CurrentPosSpread + 2.0f);
                Crosshair.ScaleCrosshair();
            }
            else
            {
                ShotGunSpread = Random.Range(2.0f, -2.0f);
            }

            m_firePoint.transform.Rotate(0, 0, ShotGunSpread, Space.Self); //Rotating the Projectile Spawn Point Angle 

            GameObject projectile = Instantiate(m_projectilePrefab, m_firePoint.position, m_firePoint.rotation);
            GameObject ProjectileMask = Instantiate(m_MaskPrefab, m_firePoint.position, m_firePoint.rotation);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            //Rigidbody2D rbMask = ProjectileMask.GetComponent<Rigidbody2D>();

            projectile.layer = 8;
           // ProjectileMask.layer = 8;

            projectile.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player");
            //ProjectileMask.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player");

            rb.AddForce(m_firePoint.up * m_projectileForce, ForceMode2D.Impulse);
            //rbMask.AddForce(m_firePoint.up * m_projectileForce, ForceMode2D.Impulse);

            m_firePoint.transform.Rotate(0, 0, (-1 * ShotGunSpread), Space.Self); //Reseting the Projectile Spawn Angle to 0 
            
            //Player cannot teleport whilst in this state
            PlayerMove.m_NoWarpCooldown = 10.0f;
        }
    }

    public void MeleeAttack()
    {
        m_MeleeHits = Physics2D.CircleCastAll(m_MeleePoint.position, m_MeleeAttackRange, transform.right, 0.0f, attackableLayer); //Melee Arc (Circle Object)

        for (int i = 0; i < m_MeleeHits.Length; i++) //Checking if the Circle Hits Antyhing 
        {
            EnemyDamageInterface iDamage = m_MeleeHits[i].collider.gameObject.GetComponent<EnemyDamageInterface>(); 

            if (iDamage != null)
            {
                
                if (m_TimeSinceMeleeHeld >= m_TimeMaxHoldMelee) //Setting the Damage to the Max Value if Charge time == max 
                {
                    m_TimeSinceMeleeHeld = m_TimeMaxHoldMelee + +0.5f;
                    
                }

                if (m_TimeSinceMeleeHeld <= 0.8f) 
                {
                    m_TimeSinceMeleeHeld = 0.5f;
                }
                else if (m_TimeSinceMeleeHeld >= 0.81f && m_TimeSinceMeleeHeld <= 1.4f)
                {
                    m_TimeSinceMeleeHeld = 1.0f;
                }
                else if (m_TimeSinceMeleeHeld >= 1.41f && m_TimeSinceMeleeHeld < 2.0f)
                {
                    m_TimeSinceMeleeHeld = 1.5f;
                }

                Debug.Log(m_TimeSinceMeleeHeld);
                iDamage.EnemyDamage(m_TimeSinceMeleeHeld);
                
            }

        }

        m_TimeSinceMeleeHeld = 0.0f;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(meeleTransform.position, meleeRange) ;
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


        //Debug.Log(m_CurrentPosSpread);
    }

    void WeaponBloom()
    {
        //Weapon Bloom 
        if (playerController.m_IsPlayerMoving == true) //This is gonna change to increase slowly by moving but for just gonna fully reset the spread 
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
    }

    public void ShotgunReload()
    {
        if (PlayerMove.m_ShotgunAmmoSupply > 0 && PlayerMove.m_ShotgunCurrentAmmo <= PlayerMove.m_ShotgunMaxAmmo)
        {
            animator.SetInteger("ammoSupply", PlayerMove.m_ShotgunAmmoSupply);
            animator.SetInteger("shellCount", PlayerMove.m_ShotgunCurrentAmmo);
            PlayerMove.m_ShotgunCurrentAmmo++;
            PlayerMove.m_ShotgunAmmoSupply--;
            


        }
    }

    void Reload() //Reloads the Gun 
    {

        switch (CurrentWeapon)
        {
            case Weapon.Weapon_Rifle:

                if (PlayerMove.m_RifleAmmoSupply > 0 && PlayerMove.m_RifleCurrentAmmo <= PlayerMove.m_RifleMaxAmmo)
                {
                    PlayerMove.m_RifleCurrentAmmo = 0;
                    for (int i = 0; i != PlayerMove.m_RifleMaxAmmo; i++)
                    {
                        if (PlayerMove.m_RifleAmmoSupply > 0)
                        {
                            PlayerMove.m_RifleAmmoSupply--;
                            PlayerMove.m_RifleCurrentAmmo++;
                        }
                    } 
                }

                break;

            case Weapon.Weapon_Shotgun:


                if (PlayerMove.m_ShotgunAmmoSupply > 0 && PlayerMove.m_ShotgunCurrentAmmo <= PlayerMove.m_ShotgunMaxAmmo)
                {
                    //ShotgunReload(); //THIS IS COMMENT BECAUSE: function is already called in animation and will 'overfill' shotgun causing loop to break
                }
                break;

            case Weapon.Weapon_Melee:
                
                break;

            default:
                Debug.Log("no weapon selected");
                break;
                
        }
       
    }

}

