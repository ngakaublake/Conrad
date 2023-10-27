using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private float m_CurrentTime = 0;
    private float m_Step = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameTimerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameTimerRoutine()
    {
       
        yield return new WaitForSeconds(m_Step);
        m_CurrentTime += m_Step;
        
    }
}
