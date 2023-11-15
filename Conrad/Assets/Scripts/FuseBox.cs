using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FuseColour
{
    Fuse_Yellow,
    Fuse_Purple,
    Fuse_Green,
    Fuse_Default,
}

public enum FuseState
{
    State_Active,
    State_Inactive,
    State_NoPower
}

public class FuseBox : MonoBehaviour
{

    public FuseColour m_FuseColour;
    public FuseState m_FuseState;

    private bool isInRange; //Checks if player is in range of pickup
    public bool isFuseActive = false;

    public CognitivePlayer Player;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerReach")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerReach")
        {
            isInRange = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
       
        if (isInRange == true && Input.GetKeyDown(KeyCode.E)) //Checking if the Player is in range of the Pickup & Checking for Player Input 
        {
            switch (m_FuseState)
            {
                case FuseState.State_Active:
                    switch (m_FuseColour) //Checking Item Type 
                    {
                        case FuseColour.Fuse_Yellow:
                            Debug.Log("Fuse Yellow Works");
                            isFuseActive = false;
                            gameObject.GetComponent<SpriteRenderer>().enabled = false;
                            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                            break;
                        case FuseColour.Fuse_Purple:
                            break;
                    }
                    break;

                case FuseState.State_Inactive:
                    switch (m_FuseColour) //Checking Item Type 
                    {
                        case FuseColour.Fuse_Yellow:
                            Debug.Log("Fuse Yellow Works off");
                            gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                            isFuseActive = true;
                            break;
                        case FuseColour.Fuse_Purple:
                            break;
                    }
                    break;
                case FuseState.State_NoPower:

                    break;
            }


            
        }
    }
}
