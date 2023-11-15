using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaseSceneEnd : MonoBehaviour
{

    private bool isPlayerInRange; //Checks if player is in range of pickup
    private bool isConradInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerReach")
        {
            isPlayerInRange = true;
           
        }

        if (collision.gameObject.tag == "ConradWinCheck" || collision.gameObject.name == "WinChecker")
        {
            isConradInRange = true;
            
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerReach")
        {
            isPlayerInRange = false;
        }

        if (collision.gameObject.tag == "ConradWinCheck")
        {
            isConradInRange = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange == true && isConradInRange == true)
        {
          
            SceneManager.LoadScene("FinalStand");
        }

        
    }
}
