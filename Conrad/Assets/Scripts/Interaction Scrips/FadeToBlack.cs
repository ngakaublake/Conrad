using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            //Make BlackScreen
            GameObject ImageGameObject = new GameObject("Fade");
            Image FadetoBlack = ImageGameObject.AddComponent<Image>();
            FadetoBlack.transform.SetParent(canvas.transform);
            RectTransform imageRectTransform = FadetoBlack.GetComponent<RectTransform>();
            imageRectTransform.anchorMin = Vector2.zero;
            imageRectTransform.anchorMax = Vector2.one;
            imageRectTransform.sizeDelta = Vector2.zero;

            // Set Colour
            FadetoBlack.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
