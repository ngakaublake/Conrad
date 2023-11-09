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
    [SerializeField] private DogBehaviour CogDog;

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
        else if (Input.GetKeyDown(KeyCode.Q) && Vector2.Distance(transform.position, playerController.transform.position) < 1f)
        {
            ShowInteractionText();
            CustomEvent();
        }
    }

    void CustomEvent()
    {
        //Some IDs refer to a custom event. When the player either enters the range or whatever, the event should trigger.
        //10XX: Scripted Event
        //20XX: One-Time Event
        switch (m_ID)
        {
            case 1001:
                //Dog Books it down Hallway, Friend Follows after a second or two
                if (CogDog != null)
                {
                    CogDog.DogBooksItDownTheHallwayScriptedEvent();
                }
                    break;
            case 2002:
                //Destroy self after three seconds- text only appears once.
                StartCoroutine(DestroyAfterDelay(3f));
                break;
            case 6:
                break;
            default:
                break;
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideInteractionText();
        Destroy(gameObject);
    }
}
