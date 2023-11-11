using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FogScript : MonoBehaviour
{
    public float m_FogSpeed = 0.2f;
    public float m_EndPosX;
    public float m_StartPosX;
    Rigidbody2D RB;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * m_FogSpeed * Time.deltaTime);

        if (transform.position.x > m_EndPosX)
        {
            float YValue = Random.Range(5.5f, -5.2f);
            transform.position = new Vector2(m_StartPosX, YValue);
        }
    }
}
