using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class RealPlayer : MonoBehaviour
{
    //Sounds
    public int m_randomSound;
    public AudioClip Foot;
    public AudioClip Foots;
    public AudioClip Feet;
    public AudioClip Feets;
    public AudioSource source;
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

    public void RandomFootstep()
    {
        m_randomSound = UnityEngine.Random.Range(1, 5);
        switch (m_randomSound)
        {
            case 1:
                source.PlayOneShot(Feet);
                break;
            case 2:
                source.PlayOneShot(Foot);
                break;
            case 3:
                source.PlayOneShot(Feets);
                break;
            default:
                source.PlayOneShot(Foots);
                break;

        }
    }
}
