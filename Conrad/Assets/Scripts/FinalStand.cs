using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStand : MonoBehaviour
{

    public float m_LevelTime = 20f;
    public float m_TimeTillEnd = 0.0f;
    public GameObject Sunrise;
    private Vector3 scaleChange = new Vector3(50f, 50f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        //m_TimeTillEnd = m_LevelTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeTillEnd -= Time.deltaTime;
        Sunrise.transform.localScale = scaleChange;

        scaleChange += new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0f);


        Debug.Log(m_TimeTillEnd);
        if (m_TimeTillEnd <= 0)
        {
            //SCRIPTED EVENT GOES HERE
            
        }
    }
}
