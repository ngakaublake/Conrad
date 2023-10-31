using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private CognitivePlayer cognitivePlayer;
    [SerializeField] private Sprite buckShot;
    public GameObject Shell;

    public Vector3 ShellPos;
    public Transform AmmoSpawn;

    GameObject newShell1;
    GameObject newShell2;
    GameObject newShell3;
    GameObject newShell4;
    GameObject newShell5;
    GameObject newShell6;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i != cognitivePlayer.m_MaxAmmo; i++) //Spawn the ammo to the UI 
        {
            AmmoSpawn.transform.Rotate(0, 0, 0, Space.Self);
            Vector3 PosOffset = new Vector3(24.0f * i, 0.0f, 0.0f); //Offset between Sprites 

            ShellPos = AmmoSpawn.transform.position + PosOffset;
            GameObject newShell = Instantiate(Shell, ShellPos, AmmoSpawn.rotation, AmmoSpawn); //Creating the Object 
            newShell.name = "ShotCount" + i; //Setting the Object Name 
        }
        
      

    }

    // Update is called once per frame
    void Update()
    {
        UpdateShotgunCount();

    }
    

    void UpdateShotgunCount()
    {
       

        for (int i = 0; i != cognitivePlayer.m_MaxAmmo; i++)
        {
            string counter = "ShotCount" + i;

            if (cognitivePlayer.m_CurrentAmmo <= i)
            {
                if (GameObject.Find(counter) == true)
                {
                    GameObject CurrentShell = GameObject.Find(counter);
                    //CurrentShell.SetActive(false);
                    CurrentShell.transform.position = new Vector3(10000f, 10000.0f, 0.0f); ;
                }
          
            }
            else
            {
                if (GameObject.Find(counter) == true)
                {
                    Debug.Log("Reload");
                    GameObject CurrentShell = GameObject.Find(counter);
                    //CurrentShell.SetActive(true);
                    Vector3 PosOffset = new Vector3(24.0f * i, 0.0f, 0.0f); //Offset between Sprites 

                    CurrentShell.transform.position = AmmoSpawn.transform.position + PosOffset;
                }
            }
        }
    }

}


