using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PickupType
{
    Pickup_None,
    Pickup_RifleAmmo,
    Pickup_ShotgunAmmo,
    Pickup_Shotgun,
    Pickup_HealthKit
}

public class Pickups : MonoBehaviour
{
    //UI Stuff 
    public string TextPopup = "Description";
    public Text UIText;
    public GameObject TextObject;


    public PickupType PickupType; //Item Type 

    private bool isInRange; //Checks if player is in range of pickup

    //Object Refs 
    public CognitivePlayer Player;

    float m_TextMaxTime = 3.0f;
    float m_TextCurrentTime = 0.0f;

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
        if (isInRange == true && Input.GetKeyDown(KeyCode.E)) //Checking if the Player is in range of the Pickup & Checking for Player Input 
        {
            

            switch (PickupType) //Checking Item Type 
            {
                case PickupType.Pickup_None:

                    break;
                case PickupType.Pickup_RifleAmmo:



                    //TranslateText();

                    transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                    for (int i = 0; i != 10; i++)
                    {
                        if (Player.m_RifleAmmoSupply < 45) 
                        {
                            
                            Player.m_RifleAmmoSupply++;
                        }
                    }

                    

                    

                    break;
                case PickupType.Pickup_ShotgunAmmo:
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                    for (int i = 0; i != 4; i++)
                    {
                        if (Player.m_ShotgunAmmoSupply < 12)
                        {
                            Player.m_ShotgunAmmoSupply++;
                        }
                    }

                    break;
                case PickupType.Pickup_Shotgun:
                    //Bool goes here to activate shotgun 
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 

                    break;
                case PickupType.Pickup_HealthKit:

                    transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                    if (Player.m_CurrentHealthKits < Player.m_MaxHealthKits)
                    {
                        Player.m_CurrentHealthKits++;
                    }
                    
                    break;
                
            }
        }
    }

    private void TranslateText()
    {
        GameObject rifleAmmoText = Instantiate(TextObject, transform.position, transform.rotation);
        TextMesh theText = rifleAmmoText.GetComponent<TextMesh>();
        theText.text = TextPopup;

        if (Time.time - m_TextCurrentTime <= m_TextMaxTime)
        {
            rifleAmmoText.transform.Translate(Vector3.up * 10f * Time.deltaTime);
            m_TextCurrentTime = Time.time;  
        }

    }
}
