using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private CognitivePlayer cognitivePlayer;

    public GameObject HeathBar;
    public Vector3 HealthBarPos;
    public Transform HealthBarSpawn;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i != cognitivePlayer.m_maxHealth; i++) //Spawn the ammo to the UI 
        {
            HealthBarSpawn.transform.Rotate(0, 0, 0, Space.Self);
            Vector3 PosOffset = new Vector3(76 * i, 0.0f, 0.0f); //Offset between Sprites 

            HealthBarPos = HealthBarSpawn.transform.position + PosOffset;
            GameObject newHealthBar = Instantiate(HeathBar, HealthBarPos, HealthBarSpawn.rotation, HealthBarSpawn); //Creating the Object 
            newHealthBar.name = "HealthBar" + i; //Setting the Object Name 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    CurrentHealthBar.transform.position = new Vector3(10000f, 10000.0f, 0.0f); ;
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

    void DisableHealthBar()
    {

    }
}
