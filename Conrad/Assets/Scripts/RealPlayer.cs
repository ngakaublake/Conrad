using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : MonoBehaviour
{
    //HI CAM the animator needed a script on the real player so here it is, hopefully its not horrible please dont hurt me i have loved ones
    private PlayerController playerController;
    public Animator animator;
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("activate");
        }

        if (playerController.m_IsPlayerMoving == true) //set walk animation speed based on if player is moving
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
