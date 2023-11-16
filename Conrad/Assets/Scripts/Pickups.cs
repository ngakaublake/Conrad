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
    private bool isInvalidPickup = false;

    //sound refs
    [SerializeField] AudioClip pickupAmmoSound;
    [SerializeField] AudioClip pickupSoftSound;
    [SerializeField] AudioClip pickupKeySound;
    [SerializeField] AudioClip pickupGunSound;
    [SerializeField] AudioSource pickupSound;

    //Object Refs 
    public CognitivePlayer Player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerShooting playerWeapon;

    float m_TextMaxTime = 1.0f;
    float m_TextCurrentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        pickupSound = GetComponentInParent<AudioSource>();
        //rifleAmmoText = Instantiate(TextObject, new Vector3(10000f, 10000.0f, 0.0f), transform.rotation, TextTransform);
        ////TextMesh theText = rifleAmmoText.GetComponent<TextMesh>();
        //Text theText = rifleAmmoText.GetComponent<Text>();
        //theText.text = TextPopup;
        //rifleAmmoText.name = TextPopup;
        //playerController = FindObjectOfType<PlayerController>();

        if (Player == null)
        {
            Player = GameObject.FindObjectOfType<CognitivePlayer>();
        }
       

        if (playerController == null)
        {
            playerController = GameObject.FindObjectOfType<PlayerController>();
        }

        if (playerWeapon == null)
        {
            playerWeapon = GameObject.FindObjectOfType<PlayerShooting>();
        }

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

                    if (Player.m_RifleAmmoSupply < 25)
                    {
                        pickupSound.PlayOneShot(pickupAmmoSound);
                        transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 

                        for (int i = 0; i != 5; i++)
                        {
                            isPickedUp = true;
                            if (Player.m_RifleAmmoSupply < 25)
                            {
                                Player.m_RifleAmmoSupply++;
                            }
                        }
                    }
                    else
                    {
                        isInvalidPickup = true;
                    }
                    break;



                case PickupType.Pickup_ShotgunAmmo:
                    

                    if (Player.m_ShotgunAmmoSupply < 6)
                    {
                        pickupSound.PlayOneShot(pickupAmmoSound);
                        transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                        for (int i = 0; i != 3; i++)
                        {
                            isPickedUp = true;
                            if (Player.m_ShotgunAmmoSupply < 6)
                            {
                                Player.m_ShotgunAmmoSupply++;
                            }
                        }
                    }
                    else
                    {
                        isInvalidPickup = true;
                    }

                    break;
                case PickupType.Pickup_Shotgun:
                    //Bool goes here to activate shotgun 
                    pickupSound.PlayOneShot(pickupGunSound);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                    isPickedUp = true;

                    playerWeapon.isShotgunActive = true;

                    break;
                case PickupType.Pickup_HealthKit:
                    if (Player.m_CurrentHealthKits < Player.m_MaxHealthKits)
                    {
                        pickupSound.PlayOneShot(pickupSoftSound);
                        isPickedUp = true;
                        transform.position = new Vector3(10000f, 10000.0f, 0.0f); //Sending to Narnia 
                        Player.m_CurrentHealthKits++;
                    }
                    else
                    {
                        isInvalidPickup = true;
                    }
                    break;

                case PickupType.Pickup_Key1:
                    pickupSound.PlayOneShot(pickupKeySound);
                    playerController.CollectKey(1);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;
                case PickupType.Pickup_Key2:
                    pickupSound.PlayOneShot(pickupKeySound);
                    playerController.CollectKey(2);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;
                case PickupType.Pickup_Key3:
                    pickupSound.PlayOneShot(pickupKeySound);
                    playerController.CollectKey(3);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;
                case PickupType.Pickup_Key4:
                    pickupSound.PlayOneShot(pickupKeySound);
                    playerController.CollectKey(4);
                    transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                    break;


            }

        }

        if (isPickedUp == true)
        {
            ShowInteractionText();
        }

        if (isInvalidPickup == true)
        {
            ShowInvalidInteractionText();
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
        Debug.Log("Pickup Test");
        m_TextCurrentTime -= Time.deltaTime;

        if (m_TextCurrentTime >= 0 && UIText != null)
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

            if (UIText != null)
            {
                UIText.text = "";
            }
            
            Destroy(gameObject);
            
        }
       
    }

    void ShowInvalidInteractionText()
    {
        Debug.Log("Pickup Test");
        m_TextCurrentTime -= Time.deltaTime;

        if (m_TextCurrentTime >= 0 && UIText != null)
        {
            Vector2 playerPosition = playerController.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)playerPosition + new Vector3((m_textPosition.x * m_TextCurrentTime), (m_textPosition.y * m_TextCurrentTime), 0f));
            //Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)playerPosition + new Vector3(m_textPosition.x + , m_textPosition.y, 0f));
            UIText.rectTransform.position = screenPosition;

            UIText.text = "Not Enough Room!"; // Set
            UIText.gameObject.SetActive(true); // Show
        }
        else
        {
            isInvalidPickup = false;
            if (UIText != null)
            {
                UIText.text = "";
            }


        }

    }
}
