using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogRenderSettings : MonoBehaviour
{
    public GameObject m_FogRef1;
    public GameObject m_FogRef2;
    private bool isInRange; //Checks if player is in range of pickup
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (isInRange == true)
        {
            m_FogRef1.GetComponent<SpriteRenderer>().enabled = false;
            m_FogRef2.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            m_FogRef1.GetComponent<SpriteRenderer>().enabled = true;
            m_FogRef2.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
