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


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i != cognitivePlayer.m_MaxAmmo; i++)
        {
            Debug.Log("Ammo Spawned");
            AmmoSpawn.transform.Rotate(0, 0, 0, Space.Self);
            Vector3 PosOffset = new Vector3 (30.0f, 0.0f, 0.0f);

            ShellPos = AmmoSpawn.transform.position + PosOffset;
            GameObject newShell = Instantiate(Shell, ShellPos, AmmoSpawn.rotation, AmmoSpawn);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //projectile.layer = 8;
}


