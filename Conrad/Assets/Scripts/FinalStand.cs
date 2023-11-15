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

    SpriteRenderer TestSunrise;
    SpriteRenderer TestFog;

    private Vector3 scaleChange = new Vector3(50f, 50f, 1f);
    public float Alpha = 0f;
    public float FogAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //m_TimeTillEnd = m_LevelTime;
        TestSunrise = SunriseSprite.GetComponent<SpriteRenderer>();
        TestFog = Fog.GetComponent<SpriteRenderer>();

        TestSunrise.color = new Color(TestSunrise.color.r, TestSunrise.color.g, TestSunrise.color.b, 0);

    }

    // Update is called once per frame
    void Update()
    {
        m_TimeTillEnd -= Time.deltaTime;
        Sunrise.transform.localScale = scaleChange;

        scaleChange += new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0f);

        Alpha += 0.01f * Time.deltaTime;
        FogAlpha -= 0.01f * Time.deltaTime;

        //Debug.Log(Alpha);

        //SpriteRenderer TestSunrise = SunriseSprite.GetComponent<SpriteRenderer>();
        Color newColor = new Color(TestSunrise.color.r, TestSunrise.color.g, TestSunrise.color.b, Alpha);

        TestFog.color = new Color(TestFog.color.r, TestFog.color.g, TestFog.color.b, FogAlpha);


        TestSunrise.color = newColor;



        Debug.Log(m_TimeTillEnd);
        if (m_TimeTillEnd <= 0)
        {
            //SCRIPTED EVENT GOES HERE
            
        }
    }
}
