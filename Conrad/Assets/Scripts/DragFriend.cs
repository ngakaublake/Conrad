using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragMode
{
    Mode_Drag,
    Mode_Drop
}

public class DragFriend : MonoBehaviour
{

    private bool isInRange; //Checks if player is in range of pickup
    public DragMode DragMode;
    public CognitivePlayer Player;
    public PlayerController PlayerController;
    Vector3 DragOffset = new Vector3(0.0f, 0.2f, 0.0f);
    public int RandomDrop;

    // Start is called before the first frame update
    void Start()
    {
        DragMode = DragMode.Mode_Drag;
        isInRange = false;
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

        if (DragMode == DragMode.Mode_Drop)
        {
            DragMode = DragMode.Mode_Drag;
        }

        if (isInRange == true && Input.GetKey(KeyCode.E) && RandomDrop != 1) //Checking if the Player is in range of the Pickup & Checking for Player Input 
        {


            switch (DragMode) //Checking Item Type 
            {
                case DragMode.Mode_Drag:
                    transform.position = Player.transform.position - DragOffset;
                    PlayerController.m_HorizontalVelocity = 0.5f;
                    PlayerController.m_VerticalVelocity = 0.5f;
                    break;
                case DragMode.Mode_Drop:
                   

                    break;
            }

            

        }

        RandomDrop = Random.Range(10, 0);

        if (RandomDrop == 1)
        {
            DragMode = DragMode.Mode_Drop;
        }

    }
}