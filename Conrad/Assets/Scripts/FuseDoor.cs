using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseDoor : MonoBehaviour
{

    public FuseBox m_FuseBoxRef; //Fuse Box Ref 
    public bool m_IsDoorOpen;
    public bool m_DoorDefaultState; //Sets the Doors Default State : TRUE = ACTIVE // FALSE = INACTIVE'
    public bool m_IsFirstTime = true;

    

    // Start is called before the first frame update
    void Start()
    {
        m_IsDoorOpen = m_DoorDefaultState; //Setting the Doors Current State to the Default 
        gameObject.GetComponent<Animator>().SetBool("IsOpen", m_IsDoorOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FuseBoxRef.isFuseActive == true ) //Checking if the Fuse is Active 
        {
            
            //if (gameObject.name == "FuseDoorYellow#1")
            //{

               
                                                                                        //gameObject.GetComponent<SpriteRenderer>().enabled = !m_DoorDefaultState; //Temp remove the sprite 
                //m_IsDoorOpen = !m_IsDoorOpen; //Setting the Current State of the Door 

                if (m_IsFirstTime == true)
                {
                    m_IsDoorOpen = !m_IsDoorOpen; //Setting the Current State of the Door 
                    Debug.Log("DOOR IS OPEN");
                //gameObject.GetComponent<Animator>().SetTrigger("DoorOpen");
                    m_FuseBoxRef.m_FuseState = FuseState.State_Active; //Setting the Fuse State 
                    gameObject.GetComponent<BoxCollider2D>().enabled = !m_DoorDefaultState; //Setting the Collider to the Oppisite of the Default State

                    gameObject.GetComponent<Animator>().SetBool("IsOpen", m_IsDoorOpen);
                    m_IsFirstTime = false;
                }
            
            //}
       


           
        }
        else if (m_FuseBoxRef.isFuseActive == false)
        {


            if (m_IsFirstTime == false)
            {
                m_IsDoorOpen = !m_IsDoorOpen; //Setting the Current State of the Door 
                m_FuseBoxRef.m_FuseState = FuseState.State_Inactive; //Setting the Fuse State 
                gameObject.GetComponent<BoxCollider2D>().enabled = m_DoorDefaultState;
                m_IsFirstTime = true;
            }


            //if (gameObject.name == "FuseDoorYellow#1")
            //{

                gameObject.GetComponent<Animator>().SetBool("IsOpen", m_IsDoorOpen);

            //}

            
        }
        
      

       
    }

    //public void Scripted1CloseDoor()
    //{
    //    m_DoorDefaultState = false;
    //    m_IsDoorOpen = m_DoorDefaultState;
    //}

    
}


