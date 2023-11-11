using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseDoor : MonoBehaviour
{

    public FuseBox m_FuseBoxRef; //Fuse Box Ref 
    private bool m_IsDoorOpen;
    public bool m_DoorDefaultState; //Sets the Doors Default State : TRUE = ACTIVE // FALSE = INACTIVE

    // Start is called before the first frame update
    void Start()
    {
        m_IsDoorOpen = m_DoorDefaultState; //Setting the Doors Current State to the Default 
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FuseBoxRef.isFuseActive == true) //Checking if the Fuse is Active 
        {
            m_FuseBoxRef.m_FuseState = FuseState.State_Active; //Setting the Fuse State 
            gameObject.GetComponent<BoxCollider2D>().enabled = !m_DoorDefaultState; //Setting the Collider to the Oppisite of the Default State
            gameObject.GetComponent<SpriteRenderer>().enabled = !m_DoorDefaultState; //Temp remove the sprite 
            m_IsDoorOpen = !m_IsDoorOpen; //Setting the Current State of the Door 
        }
        else if (m_FuseBoxRef.isFuseActive == false)
        {
            m_FuseBoxRef.m_FuseState = FuseState.State_Inactive; //Setting the Fuse State 
            gameObject.GetComponent<BoxCollider2D>().enabled = m_DoorDefaultState;
            gameObject.GetComponent<SpriteRenderer>().enabled = m_DoorDefaultState;
            m_IsDoorOpen = !m_IsDoorOpen; //Setting the Current State of the Door 
        }
      

       
    }

    
}


