using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHitbox : MonoBehaviour
{
    public string m_InteractionText;
    public int m_ID;
    public bool m_AutoShow;
    private PlayerController playerController;
    [SerializeField] private Vector2 m_textPosition;
    [SerializeField] private Text interactionText;

    // Start is called before the first frame update
    void Start()
    {
        HideInteractionText();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AutoShow)
        {
            if (Vector2.Distance(transform.position, playerController.transform.position) < 1f)
            {
                ShowInteractionText();
            }
        }
        else if (Input.GetKeyDown(KeyCode.G) && Vector2.Distance(transform.position, playerController.transform.position) < 1f)
        {
            ShowInteractionText();
        }
    }
    void ShowInteractionText()
    {
        interactionText.text = m_InteractionText; // Set
        interactionText.rectTransform.anchoredPosition = m_textPosition; // Move
        interactionText.gameObject.SetActive(true); // Show
        UnityEngine.Debug.Log("Interaction Text: " + m_InteractionText);
    }
    void HideInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
