using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float m_FogOpacity;
    public bool m_IsRenderingCognitiveWorld;
    // Start is called before the first frame update
    void Start()
    {
        m_FogOpacity = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsRenderingCognitiveWorld)
        {
            m_FogOpacity = 1.0f;
        }
        else
        {
            m_FogOpacity = 0.0f;
        }
        Color FogColor = spriteRenderer.color;
        FogColor.a = m_FogOpacity;
        spriteRenderer.color = FogColor;
        
    }
}
