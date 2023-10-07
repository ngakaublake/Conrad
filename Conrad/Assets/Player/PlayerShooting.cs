using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMove;

    public Transform m_firePoint;
    public GameObject m_projectilePrefab;

    public float m_projectileForce = 20f; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject projectile = Instantiate(m_projectilePrefab, m_firePoint.position, m_firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(m_firePoint.up * m_projectileForce, ForceMode2D.Impulse);
    }

    void GetProjectileSpread()
    {
    }
}

