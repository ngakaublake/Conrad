using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corpse : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TurnOfAnimator()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
