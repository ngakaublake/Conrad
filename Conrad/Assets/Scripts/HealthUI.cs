using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class HealthUI : MonoBehaviour
{
    //Player Refrence 
    [SerializeField] private CognitivePlayer cognitivePlayer;
    [SerializeField] private ConradHealScript conradActIII;

    //Health Bar
    public GameObject HeathBar;
    public Vector3 HealthBarPos;
    public Transform HealthBarSpawn;

    //ConradBar
    public GameObject ConradHP;
    private bool Healthshown;

    //Health Kit 
    public Vector3 HealthKitPos;
    public GameObject HealthKit;
    public GameObject HealthKitEmpty;
    public Transform HealthKitSpawn;
    public Text HealthKitCounter; 

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i != cognitivePlayer.m_maxHealth; i++) //Spawn the Health Bar to the UI 
        {
            HealthBarSpawn.transform.Rotate(0, 0, 0, Space.Self);
            Vector3 PosOffset = new Vector3(76 * i, 0.0f, 0.0f); //Offset between Sprites 

            HealthBarPos = HealthBarSpawn.transform.position + PosOffset;
            GameObject newHealthBar = Instantiate(HeathBar, HealthBarPos, HealthBarSpawn.rotation, HealthBarSpawn); //Creating the Object 
            newHealthBar.name = "HealthBar" + i; //Setting the Object Name 
        }
        if (conradActIII != null)
        {
            Healthshown = false;
            for (int i = 0; i != conradActIII.m_maxHealth; i++) //Spawn Conrad Health Bar
            {
                HealthBarSpawn.transform.Rotate(0, 0, 0, Space.Self);
                Vector2 PosOffset = new Vector2(2.0f * i, 2.0f); //Offset between Sprites 
                Vector2 conradPosition = conradActIII.transform.position;
                HealthBarPos  = conradPosition + PosOffset;
                GameObject newConradHP = Instantiate(ConradHP, HealthBarPos, HealthBarSpawn.rotation);
                newConradHP.name = "ConradHP" + i;
            }
        }
        
        //Spawning the Health Kit to the UI 
        HealthKitSpawn.transform.Rotate(0, 0, 0, Space.Self);
        HealthKitPos = HealthKitSpawn.transform.position;
        GameObject newHealthKit = Instantiate(HealthKit, HealthKitPos, HealthKitSpawn.rotation, HealthKitSpawn); //Creating the Object 
        newHealthKit.name = "HealthKit";


        HealthKitSpawn.transform.Rotate(0, 0, 0, Space.Self);
        HealthKitPos = HealthKitSpawn.transform.position;
        GameObject newHealthKitUsed = Instantiate(HealthKitEmpty, HealthKitPos, HealthKitSpawn.rotation, HealthKitSpawn); //Creating the Object 
        newHealthKitUsed.name = "HealthKitUsed";



    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        UpdateHealthKits();
        if (conradActIII != null)
        {
            UpdateConradHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        for (int i = 0; i != cognitivePlayer.m_maxHealth; i++)
        {
            string counter = "HealthBar" + i;

            if (cognitivePlayer.m_health <= i)
            {
                if (GameObject.Find(counter) == true)
                {
                    GameObject CurrentHealthBar = GameObject.Find(counter);
                    //CurrentShell.SetActive(false);
                    CurrentHealthBar.transform.position = new Vector3(10000f, 10000.0f, 0.0f); 
                }
            }
            else
            {
                if (GameObject.Find(counter) == true)
                {

                    GameObject CurrentHealthBar = GameObject.Find(counter);
                    //CurrentShell.SetActive(true);
                    Vector3 PosOffset = new Vector3(76.0f * i, 0.0f, 0.0f); //Offset between Sprites 

                    CurrentHealthBar.transform.position = HealthBarSpawn.transform.position + PosOffset;
                }
            }
        }
    }

    void UpdateConradHealthBar()
    {
        for (int i = 0; i != conradActIII.m_maxHealth; i++)
        {
            string counter = "ConradHP" + i;

            if (conradActIII.m_health <= i || !Healthshown)
            {
                if (GameObject.Find(counter) == true)
                {
                    GameObject ConradBar = GameObject.Find(counter);
                    //CurrentShell.SetActive(false);
                    ConradBar.transform.position = new Vector3(10000f, 10000.0f, 0.0f);
                }
            }
            else
            {
                if (GameObject.Find(counter) == true)
                {

                    GameObject ConradBar = GameObject.Find(counter);
                    //CurrentShell.SetActive(true);
                    Vector2 PosOffset = new Vector2(-0.5f+(0.3f * i), 0.6f); //Offset between Sprites 
                    Vector2 conradPosition = conradActIII.transform.position;
                    ConradBar.transform.position = conradPosition + PosOffset;
                }
            }
        }
    }

    void UpdateHealthKits()
    {
        HealthKitCounter.text = "x" + cognitivePlayer.m_CurrentHealthKits.ToString();

        if (cognitivePlayer.m_CurrentHealthKits == 0)
        {
            if (GameObject.Find("HealthKitUsed") == true)
            {
                GameObject UsedtHealthKit = GameObject.Find("HealthKitUsed");
               
                UsedtHealthKit.transform.position = HealthKitSpawn.transform.position;
            }

        }
        else
        {
            if (GameObject.Find("HealthKitUsed") == true)
            {
                GameObject UsedtHealthKit = GameObject.Find("HealthKitUsed");
               
                UsedtHealthKit.transform.position = new Vector3(10000f, 10000.0f, 0.0f);
            }
        }
    }

    void DisableHealthBar()
    {

    }

    public void ShowConradHealth()
    {
        Healthshown = true;
    }
}

