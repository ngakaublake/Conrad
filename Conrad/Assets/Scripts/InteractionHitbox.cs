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
    public bool m_isInRange;
    private PlayerController playerController;
    [SerializeField] private Vector2 m_textPosition;
    [SerializeField] private Text interactionText;

    // Start is called before the first frame update
    void Start()
    {
        HideInteractionText();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_isInRange = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        ProcessInteraction();
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

    void ProcessInteraction()
    {
        if (m_AutoShow)
        {
            if (Vector2.Distance(transform.position, playerController.transform.position) < 1f)
            {
                ShowInteractionText();
                CustomEvent();
            }
        }
        else if (Input.GetKeyDown(KeyCode.G) && Vector2.Distance(transform.position, playerController.transform.position) < 1f)
        {
            ShowInteractionText();
            CustomEvent();
        }
    }

    void CustomEvent()
    {
        //Some IDs refer to a custom event. When the player either enters the range or whatever, the event should trigger.
        switch (m_ID)
        {
            case 1001:
                break;
            case 1002:
                break;
            case 6:
                break;
            default:
                break;
        }
    }
}
