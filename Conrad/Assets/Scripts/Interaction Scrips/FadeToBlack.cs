using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] public GameObject Parent;
    private Image FadetoBlack;
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            //Make BlackScreen
            GameObject ImageGameObject = new GameObject("Fade");
            FadetoBlack = ImageGameObject.AddComponent<Image>();
            FadetoBlack.transform.SetParent(Parent.transform);

            RectTransform imageRectTransform = FadetoBlack.GetComponent<RectTransform>();
            imageRectTransform.anchorMin = Vector2.zero;
            imageRectTransform.anchorMax = Vector2.one;
            imageRectTransform.anchoredPosition = Vector2.zero;
            imageRectTransform.sizeDelta = Vector2.zero;

            // Set Colour
            FadetoBlack.color = Color.black;
            Canvas canvasComponent = canvas.GetComponent<Canvas>();
            FadetoBlack.color = new Color(0, 0, 0, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1.0f, 0.0f, 2.0f));
    }
    public void FadeOut()
    {
        StartCoroutine(Fade(0.0f, 1.0f, 2.0f));
    }


    private IEnumerator Fade(float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = FadetoBlack.color;

        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (elapsedTime < duration)
        {
            FadetoBlack.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        FadetoBlack.color = targetColor; // Ensure final color is set
    }

}
