using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //unity ui for ammo counter

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private CognitivePlayer cognitivePlayer;
    
    public GameObject ShotgunShell;
    public GameObject RifleBullet;

    public Vector3 ShellPos;
    public Transform AmmoSpawn;

    public Text ammoCounter; //ammocounter UI

    // Start is called before the first frame update
    void Start()
    {

        //Shotgun 
        for (int i = 0; i != cognitivePlayer.m_ShotgunMaxAmmo; i++) //Spawn the ammo to the UI 
        {
            AmmoSpawn.transform.Rotate(0, 0, 0, Space.Self);
            Vector3 PosOffset = new Vector3(40.0f * i, 0.0f, 0.0f); //Offset between Sprites 

            ShellPos = AmmoSpawn.transform.position + PosOffset;
            GameObject newShell = Instantiate(ShotgunShell, ShellPos, AmmoSpawn.rotation, AmmoSpawn); //Creating the Object 
            newShell.name = "ShotgunShotCount" + i; //Setting the Object Name 
        }

        //Rifle 
        for (int i = 0; i != cognitivePlayer.m_RifleMaxAmmo; i++) //Spawn the ammo to the UI 
        {
            AmmoSpawn.transform.Rotate(0, 0, 0, Space.Self);
            Vector3 PosOffset = new Vector3(24.0f * i, 0.0f, 0.0f); //Offset between Sprites 

            ShellPos = AmmoSpawn.transform.position + PosOffset;
            GameObject newShell = Instantiate(RifleBullet, ShellPos, AmmoSpawn.rotation, AmmoSpawn); //Creating the Object 
            newShell.name = "RifleShotCount" + i; //Setting the Object Name 
        }

        DisableShotgunCount();
        DisableRifleCount();

    }

    // Update is called once per frame
    void Update()
    {

        switch (playerShooting.CurrentWeapon)
        {
            case Weapon.Weapon_Rifle:
                UpdateRifleCount();
                break;

            case Weapon.Weapon_Shotgun:
                UpdateShotgunCount();
                break;

            case Weapon.Weapon_Melee:

                break;

            default:
                Debug.Log("no weapon selected");
                break;

        }
        

    }
    

    void UpdateShotgunCount()
    {
        for (int i = 0; i != cognitivePlayer.m_ShotgunMaxAmmo; i++)
        {
            string counter = "ShotgunShotCount" + i;

            if (cognitivePlayer.m_ShotgunCurrentAmmo <= i)
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
                    
                    GameObject CurrentShell = GameObject.Find(counter);
                    //CurrentShell.SetActive(true);
                    Vector3 PosOffset = new Vector3(40.0f * i, 0.0f, 0.0f); //Offset between Sprites 

                    CurrentShell.transform.position = AmmoSpawn.transform.position + PosOffset;
                }
            }
        }
        ammoCounter.text = cognitivePlayer.m_ShotgunAmmoSupply.ToString();
    }


    void UpdateRifleCount()
    {


        for (int i = 0; i != cognitivePlayer.m_RifleMaxAmmo; i++)
        {
            string counter = "RifleShotCount" + i;

            if (cognitivePlayer.m_RifleCurrentAmmo <= i)
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
                    //Debug.Log("Reload");
                    GameObject CurrentShell = GameObject.Find(counter);
                    //CurrentShell.SetActive(true);
                    Vector3 PosOffset = new Vector3(24.0f * i, 0.0f, 0.0f); //Offset between Sprites 

                    CurrentShell.transform.position = AmmoSpawn.transform.position + PosOffset;
                }
            }
        }

        ammoCounter.text = cognitivePlayer.m_RifleAmmoSupply.ToString();
    }

    public void DisableShotgunCount()
    {

        for (int i = 0; i != cognitivePlayer.m_ShotgunMaxAmmo; i++)
        {
            string counter = "ShotgunShotCount" + i;

            if (GameObject.Find(counter) == true)
            {
                GameObject CurrentShell = GameObject.Find(counter);
                CurrentShell.transform.position = new Vector3(10000f, 10000.0f, 0.0f);
            }

        }

    }

    public void DisableRifleCount()
    {

        for (int i = 0; i != cognitivePlayer.m_RifleMaxAmmo; i++)
        {
            string counter = "RifleShotCount" + i;

            if (GameObject.Find(counter) == true)
            {
                GameObject CurrentShell = GameObject.Find(counter);
                CurrentShell.transform.position = new Vector3(10000f, 10000.0f, 0.0f);
            }

        }

    }

}


