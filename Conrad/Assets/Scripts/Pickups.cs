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
    public string TextPopup = "Description";
    public Text UIText;
    public PickupType PickupType;
    private bool isInRange;

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
        if (isInRange == true && Input.GetKeyDown(KeyCode.E))
        {
            transform.position = new Vector3(10000f, 10000.0f, 0.0f);
        }
    }
}
