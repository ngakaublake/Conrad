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
    [SerializeField] private Vector2 m_textPosition;
    //public GameObject TextObject;
    //public Transform TextTransform;
    //private GameObject rifleAmmoText;


    public PickupType PickupType; //Item Type 

    private bool isInRange; //Checks if player is in range of pickup
    private bool isPickedUp = false; 


    //Object Refs 
    public CognitivePlayer Player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerShooting playerWeapon;

    float m_TextMaxTime = 1.0f;
    float m_TextCurrentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //rifleAmmoText = Instantiate(TextObject, new Vector3(10000f, 10000.0f, 0.0f), transform.rotation, TextTransform);
        ////TextMesh theText = rifleAmmoText.GetComponent<TextMesh>();
        //Text theText = rifleAmmoText.GetComponent<Text>();
        //theText.text = TextPopup;
        //rifleAmmoText.name = TextPopup;
        //playerController = FindObjectOfType<PlayerController>();

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

            m_TextCurrentTime = m_TextMaxTime; 

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
                        isPickedUp = true;
                        if (Player.m_ShotgunAmmoSupply < 12)
                        {
                            Player.m_ShotgunAmmoSupply++;
                        }
                    }

                    break;
                case PickupType.Pickup_Shotgun:
                    //Bool goes here to activate shotgun 
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                    isPickedUp = true;

                    playerWeapon.isShotgunActive = true;

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
            Debug.Log("show text");
            ShowInteractionText();

            //DestroyAfterDelay(5.0f);
        }
    }

    private void TranslateText()
    {
        //rifleAmmoText.transform.Translate(Vector3.up * 10f * Time.deltaTime);
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        //Deletes object after 'delay' seconds.
        //Used to delete BulletMask as it doesn't collide
        yield return new WaitForSeconds(delay);
        //Destroy(UIText);
        Debug.Log("TEST DESTROY");
        gameObject.GetComponent<Pickups>().UIText.enabled = false;
    }

    void ShowInteractionText()
    {
        m_TextCurrentTime -= Time.deltaTime;

        if (m_TextCurrentTime >= 0)
        {
            Vector2 playerPosition = playerController.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)playerPosition + new Vector3((m_textPosition.x * m_TextCurrentTime) , (m_textPosition.y * m_TextCurrentTime), 0f));
            //Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)playerPosition + new Vector3(m_textPosition.x + , m_textPosition.y, 0f));
            UIText.rectTransform.position = screenPosition;

            UIText.text = TextPopup; // Set
            UIText.gameObject.SetActive(true); // Show
        }
        else
        {
            Debug.Log("TEST DESTROY");
            //gameObject.GetComponent<Pickups>().UIText.enabled = false;
            UIText.text = "";
        }
       
    }
}
