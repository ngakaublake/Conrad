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


        if (isFuseActive == true)
        {
            m_FuseState = FuseState.State_Active;
        }
        else if (isFuseActive == true)
        {
            m_FuseState = FuseState.State_Inactive;
        }

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
                            //gameObject.GetComponent<AudioSource>().Play();
                            gameObject.GetComponentInChildren<AudioSource>().Play();
                            break;
                        case FuseColour.Fuse_Purple:
                            break;
                    }
                    break;

                case FuseState.State_Inactive:
                    switch (m_FuseColour) //Checking Item Type 
                    {
                        case FuseColour.Fuse_Yellow:
                            isFuseActive = true;
                            Debug.Log("Fuse Yellow Works off");
                            
                            gameObject.GetComponentInChildren<AudioSource>().Play();

                            gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                            
                            
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
