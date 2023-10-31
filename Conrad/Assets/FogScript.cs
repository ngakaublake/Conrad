using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FogScript : MonoBehaviour
{

    Rigidbody2D RB;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     

        if (transform.position.x > 5)
        {
            transform.position = new Vector2(0, 0);
        }
    }
}
