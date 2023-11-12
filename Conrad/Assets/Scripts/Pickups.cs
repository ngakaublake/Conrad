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
    Pickup_HealthKit,
    Pickup_Key1,
    Pickup_Key2,
    Pickup_Key3,
    Pickup_Key4
}

public class Pickups : MonoBehaviour
{
    //UI Stuff 
    public string TextPopup = "Description";
    public Text UIText;
    public GameObject TextObject;
    public Transform TextTransform;
    private GameObject rifleAmmoText;


    public PickupType PickupType; //Item Type 

    private bool isInRange; //Checks if player is in range of pickup
    private bool isPickedUp = false; 


    //Object Refs 
    public CognitivePlayer Player;
    private PlayerController playerController;

    float m_TextMaxTime = 3.0f;
    float m_TextCurrentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rifleAmmoText = Instantiate(TextObject, new Vector3(10000f, 10000.0f, 0.0f), transform.rotation, TextTransform);
        //TextMesh theText = rifleAmmoText.GetComponent<TextMesh>();
        Text theText = rifleAmmoText.GetComponent<Text>();
        theText.text = TextPopup;
        rifleAmmoText.name = TextPopup;
        playerController = FindObjectOfType<PlayerController>();

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

                    

                    

                    transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                    for (int i = 0; i != 10; i++)
                    {
                        if (Player.m_RifleAmmoSupply < 45) 
                        {
                            isPickedUp = true;
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

                    
                    if (Player.m_CurrentHealthKits < Player.m_MaxHealthKits)
                    {
                        transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                        Player.m_CurrentHealthKits++;
                    }
                    
                    break;
                case PickupType.Pickup_Key1:
                    playerController.CollectKey(1);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;
                case PickupType.Pickup_Key2:
                    playerController.CollectKey(2);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;
                case PickupType.Pickup_Key3:
                    playerController.CollectKey(3);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;
                case PickupType.Pickup_Key4:
                    playerController.CollectKey(4);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;
            }
        }

        if (isPickedUp == true)
        {
            TranslateText();

            DestroyAfterDelay(5.0f);
        }
    }

    private void TranslateText()
    {
        rifleAmmoText.transform.Translate(Vector3.up * 10f * Time.deltaTime);
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        //Deletes object after 'delay' seconds.
        //Used to delete BulletMask as it doesn't collide
        yield return new WaitForSeconds(delay);
        Destroy(rifleAmmoText);
    }
}
