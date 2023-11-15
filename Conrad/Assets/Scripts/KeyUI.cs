using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public Transform KeyPlacement;
    public Vector3 KeyPlacePos1;
    public Vector3 KeyPlacePos2;
    public Vector3 KeyPlacePos3;
    public Vector3 KeyPlacePos4;
    public Vector3 KeyPlacePos5;
    public Vector3 KeyVoidPlacement;
    public GameObject Key;
    public Sprite s_Key_1;
    public Sprite s_Key_2;
    public Sprite s_Key_3;
    public Sprite s_Key_4;
    public Sprite s_Key;
    private int m_keycount;
    private string m_counter;
    private bool showkey1;
    private bool showkey2;
    private bool showkey3;
    private bool showkey4;
    private bool showwholekey;
    // Start is called before the first frame update
    void Start()
    {
        KeyPlacement.transform.Rotate(0, 0, 0, Space.Self);
        KeyPlacePos1 = KeyPlacement.transform.position + new Vector3(1610.0f, -980.0f, 0.0f);
        KeyPlacePos2 = KeyPlacement.transform.position + new Vector3(1670.0f, -980.0f, 0.0f);
        KeyPlacePos3 = KeyPlacement.transform.position + new Vector3(1730.0f, -980.0f, 0.0f);
        KeyPlacePos4 = KeyPlacement.transform.position + new Vector3(1790.0f, -980.0f, 0.0f);
        KeyPlacePos5 = KeyPlacement.transform.position + new Vector3(1790.0f, -980.0f, 0.0f);

        KeyVoidPlacement = new Vector3(10000f, 10000.0f, 0.0f);

        GameObject Key1UI = Instantiate(Key, KeyPlacePos1, KeyPlacement.rotation, KeyPlacement);
        GameObject Key2UI = Instantiate(Key, KeyPlacePos2, KeyPlacement.rotation, KeyPlacement);
        GameObject Key3UI = Instantiate(Key, KeyPlacePos3, KeyPlacement.rotation, KeyPlacement);
        GameObject Key4UI = Instantiate(Key, KeyPlacePos4, KeyPlacement.rotation, KeyPlacement);
        GameObject Key5UI = Instantiate(Key, KeyPlacePos5, KeyPlacement.rotation, KeyPlacement);
        Image key1Image = Key1UI.GetComponent<Image>();
        Image key2Image = Key2UI.GetComponent<Image>();
        Image key3Image = Key3UI.GetComponent<Image>();
        Image key4Image = Key4UI.GetComponent<Image>();
        Image key5Image = Key5UI.GetComponent<Image>();
        if (key1Image != null) { key1Image.sprite = s_Key_1; }
        if (key2Image != null) { key2Image.sprite = s_Key_2; }
        if (key3Image != null) { key3Image.sprite = s_Key_3; }
        if (key4Image != null) { key4Image.sprite = s_Key_4; }
        if (key5Image != null) { key5Image.sprite = s_Key; }

        Key1UI.name = "KeyUI1";
        Key2UI.name = "KeyUI2";
        Key3UI.name = "KeyUI3";
        Key4UI.name = "KeyUI4";
        Key5UI.name = "KeyFullUI";

        showkey1 = false; showkey2 = false; showkey3 = false; showkey4 = false;
        showwholekey = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();
        SetKeys();
    }

    void CheckKeys()
    {
        m_keycount = 0;
        if (playerController.m_key1Obtained == true)
        { m_keycount++; showkey1 = true; }
        else { showkey1 = false; }
        if (playerController.m_key2Obtained == true)
        { m_keycount++; showkey2 = true; }
        else { showkey2 = false; }
        if (playerController.m_key3Obtained == true)
        { m_keycount++; showkey3 = true; }
        else { showkey3 = false; }
        if (playerController.m_key4Obtained == true)
        { m_keycount++; showkey4 = true; }
        else { showkey4 = false; }
        if (m_keycount == 4)
        {
            showkey1 = false; showkey2 = false; showkey3 = false; showkey4 = false;
            showwholekey = true;
            playerController.CollectKey(5);
        }
        else
        {
            showwholekey = false;
        }
    }

    void SetKeys()
    {
        GameObject Key1 = GameObject.Find("KeyUI1");
        GameObject Key2 = GameObject.Find("KeyUI2");
        GameObject Key3 = GameObject.Find("KeyUI3");
        GameObject Key4 = GameObject.Find("KeyUI4");
        GameObject Key5 = GameObject.Find("KeyFullUI");
        if (showkey1) 
        {
            Key1.transform.position = KeyPlacePos1;
        }
        else
        {
            Key1.transform.position = KeyVoidPlacement;
        }

        if (showkey2) 
        {
            Key2.transform.position = KeyPlacePos2;
        }
        else
        {
            Key2.transform.position = KeyVoidPlacement;
        }

        if (showkey3) 
        {
            Key3.transform.position = KeyPlacePos3;
        }
        else
        {
            Key3.transform.position = KeyVoidPlacement;
        }  

        if (showkey4) 
        {
            Key4.transform.position = KeyPlacePos4;
        }
        else
        {
            Key4.transform.position = KeyVoidPlacement;
        }

        if (showwholekey) 
        { 
            Key5.transform.position = KeyPlacePos5;
        }
        else
        {
            Key5.transform.position = KeyVoidPlacement;
        }
    }
}
