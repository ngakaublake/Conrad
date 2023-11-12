using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    public int CheckpointID;
    private PlayerController playerController;

    private bool Checkpoint1Triggered;
    private bool Checkpoint2Triggered;
    private bool Checkpoint3Triggered;
    private bool Checkpoint4Triggered;
    private bool Checkpoint5Triggered;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        Checkpoint1Triggered = false;
        Checkpoint2Triggered = false;
        Checkpoint3Triggered = false;
        Checkpoint4Triggered = false;
        Checkpoint5Triggered = false;
    }
    // Update is called once per frame
    void Update()
    {
        ProcessInteraction();
    }

    void ProcessInteraction()
    {
        if (Vector2.Distance(transform.position, playerController.transform.position) < 1f)
        {
            CheckID();
        }


    }

    void CheckID()
    {
        //ID used for Checkpoints
        switch (CheckpointID)
        {
            case 1:
                //Autoset when you enter this area for the first time
                if (!Checkpoint1Triggered)
                {
                    playerController.CheckpointUpdater(new Vector2(0.5f, -0.5f));
                    Checkpoint1Triggered = true;
                }
                else if (playerController.m_key5Obtained && !Checkpoint2Triggered)
                {
                    playerController.CheckpointUpdater(new Vector2(0.5f, -0.5f));
                    Checkpoint2Triggered = true;
                }
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

}