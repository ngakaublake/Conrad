using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseLights : MonoBehaviour
{

    public FuseBox m_FuseBoxRef; //Fuse Box Ref 
    private bool m_IsLightOn;
    public bool m_LightDefaultState; //Sets the Lights Default State : TRUE = ACTIVE // FALSE = INACTIVE
    public bool m_DoLightsFlicker;

    // Start is called before the first frame update
    void Start()
    {
        m_IsLightOn = m_LightDefaultState;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FuseBoxRef.isFuseActive == true) //Checking if the Fuse is Active 
        {
            m_FuseBoxRef.m_FuseState = FuseState.State_Active; //Setting the Fuse State 
            gameObject.GetComponent<MeshRenderer>().enabled = !m_LightDefaultState; //Temp remove the sprite 
            m_IsLightOn = !m_IsLightOn; //Setting the Current State of the Door 
        }
        else if (m_FuseBoxRef.isFuseActive == false)
        {
            m_FuseBoxRef.m_FuseState = FuseState.State_Inactive; //Setting the Fuse State 
            gameObject.GetComponent<MeshRenderer>().enabled = m_LightDefaultState;
            m_IsLightOn = !m_IsLightOn; //Setting the Current State of the Door 
        }


        //if (m_LightDefaultState = m_FuseBoxRef.isFuseActive && m_DoLightsFlicker == true)
        //{
        //    FlickerLights();
        //}
    }

    void FlickerLights()
    {
        float LightFlicker = Random.Range(0, 1000);
        Debug.Log("Flicker Lights");
        if (LightFlicker < 5)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
