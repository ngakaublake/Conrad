using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public PlayerShooting playerShooting;
    AudioSource source;

    public AudioClip RifleFire;
    public AudioClip ShotgunFire;

    public AudioClip RifleChamber;
    public AudioClip ShotgunChamber;

    public AudioClip RifleReload;
    public AudioClip ShotgunReload01;
    public AudioClip ShotgunReload02;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void playFireSound()
    {
        switch (playerShooting.CurrentWeapon)
        {
            case Weapon.Weapon_Rifle:
                source.PlayOneShot(RifleFire);
                break;

            case Weapon.Weapon_Shotgun:
                source.PlayOneShot(ShotgunFire);
                break;

            default:
                Debug.Log("no weapon selected");
                break;

        }
    }

    void playChamberSound()
    {
        switch (playerShooting.CurrentWeapon)
        {
            case Weapon.Weapon_Rifle:
                source.PlayOneShot(RifleChamber);
                break;

            case Weapon.Weapon_Shotgun:
                source.PlayOneShot(ShotgunChamber);
                break;

            default:
                Debug.Log("no weapon selected");
                break;

        }
    }

    void playReloadSound()
    {
        switch (playerShooting.CurrentWeapon)
        {
            case Weapon.Weapon_Rifle:
                source.PlayOneShot(RifleReload);
                break;

            case Weapon.Weapon_Shotgun:
                source.PlayOneShot(ShotgunReload01);
                break;

            default:
                Debug.Log("no weapon selected");
                break;

        }
    }

    void playShellSound()
    {
        source.PlayOneShot(ShotgunReload02);
    }
}
