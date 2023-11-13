using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITeleport : MonoBehaviour
{
    [SerializeField] private CognitivePlayer cognitivePlayer;
    public GameObject UIElement;
    public Vector3 UIElementPos;
    public Transform UIElementSpawn;


    // Start is called before the first frame update
    void Start()
    {
        UIElementSpawn.transform.Rotate(0, 0, 0, Space.Self);
        UIElementPos = UIElementSpawn.transform.position;
        GameObject newHealthKit = Instantiate(UIElement, UIElementPos, UIElementSpawn.rotation, UIElementSpawn); //Creating the Object 
        newHealthKit.name = "TeleportUI";

    }

    // Update is called once per frame
    void Update()
    {
        UpdateWarp();
    }

    void UpdateWarp()
    {
        if (cognitivePlayer.m_CanWarp == false)
        {
            if (GameObject.Find("TeleportUI") == true)
            {
                GameObject UIElement = GameObject.Find("TeleportUI");
                //CurrentShell.SetActive(false);
                UIElement.transform.position = new Vector3(10000f, 10000.0f, 0.0f);
            }
        }
        else
        {
            if (GameObject.Find("TeleportUI") == true)
            {
                GameObject UIElement = GameObject.Find("TeleportUI");
                //CurrentShell.SetActive(false);
                UIElement.transform.position = UIElementSpawn.position;
            }
        }
    }
}
