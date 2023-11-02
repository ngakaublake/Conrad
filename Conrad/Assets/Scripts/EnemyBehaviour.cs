using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float minimumDistance = 0.9f;
    //int health = 10;

    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerController.m_CognitiveWorldPosition) > minimumDistance)
        {
           transform.position = Vector2.MoveTowards(transform.position, playerController.m_CognitiveWorldPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            //Enemy Attack when in range
        }
    }
}
