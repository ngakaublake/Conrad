using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStand : MonoBehaviour
{

    public float m_LevelTime = 20f;
    public float m_TimeTillEnd = 0.0f;
    public GameObject Sunrise;
    public GameObject SunriseSprite;
    public GameObject Fog;
    private PlayerController playerController;
    [SerializeField] private ScriptedEvent4 FinalPush;

    SpriteRenderer TestSunrise;
    SpriteRenderer TestFog;

    private Vector3 scaleChange = new Vector3(50f, 50f, 1f);
    public float Alpha = 0f;
    public float FogAlpha = 0.7921569f;

    public float m_TimeSunrise = 3.0f;
    public float m_CurrentSunTime;

    // Start is called before the first frame update
    void Start()
    {
        //m_TimeTillEnd = m_LevelTime;
        TestSunrise = SunriseSprite.GetComponent<SpriteRenderer>();
        TestFog = Fog.GetComponent<SpriteRenderer>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        TestSunrise.color = new Color(TestSunrise.color.r, TestSunrise.color.g, TestSunrise.color.b, 0);
        m_CurrentSunTime = m_TimeSunrise;
        m_TimeTillEnd = 20f;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.m_CognitiveWorldResetting)
        {
            TestSunrise.color = new Color(TestSunrise.color.r, TestSunrise.color.g, TestSunrise.color.b, 0);
            m_CurrentSunTime = m_TimeSunrise;
            m_TimeTillEnd = 16f;
        }


        m_TimeTillEnd -= Time.deltaTime/14;
       

        //Sunrise.transform.localScale = scaleChange;
        //scaleChange += new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0f);

       
        if (Alpha < 0.25)
        {
            Alpha += 0.1f * Time.deltaTime / 40;
        }

        if (FogAlpha > 0.3)
        {
            FogAlpha += Time.deltaTime / 10 * -0.1f;
        }

           
        //SpriteRenderer TestSunrise = SunriseSprite.GetComponent<SpriteRenderer>();
        TestSunrise.color = new Color(TestSunrise.color.r, TestSunrise.color.g, TestSunrise.color.b, Alpha);
        TestFog.color = new Color(TestFog.color.r, TestFog.color.g, TestFog.color.b, FogAlpha);

        Debug.Log(m_TimeTillEnd);
        if (m_TimeTillEnd <= 0)
        {
            FinalPush.ProcessInteraction();
        }
    }
}
