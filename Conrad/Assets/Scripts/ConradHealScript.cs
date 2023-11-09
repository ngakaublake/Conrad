using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConradHealScript : MonoBehaviour
{
    private int m_Health;
    // Start is called before the first frame update
    void Start()
    {
        m_Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseHealth()
    {
        m_Health = 4;
    }
    public void LoseHealth()
    {
        m_Health--;
    }
}
