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
        HideInteractionText();
    }


    // Update is called once per frame
    void Update()
    {
        ProcessInteraction();
    }
    void ShowInteractionText()
    {
        interactionText.text = m_InteractionText; // Set
        interactionText.gameObject.SetActive(true); // Show
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
                Vector2 playerPosition = playerController.transform.position;
                Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)playerPosition + new Vector3(m_textPosition.x, m_textPosition.y, 0f));
                interactionText.rectTransform.position = screenPosition;
                ShowInteractionText();
                CustomEvent();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, playerController.transform.position) < 1f)
        {
            StartCoroutine(MoveTextTowardsPlayer());
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
            case 2002:
                //Destroy self after three seconds- text only appears once.
                StartCoroutine(DestroyAfterDelay(3f));
                break;
            case 2001:
                //Destroy self after three seconds- text only appears once.
                if (playerController.m_key2Obtained == true || playerController.m_key1Obtained == true || playerController.m_key4Obtained == true)
                {
                    m_InteractionText = "This is another part of that key.";
                }
                playerController.CollectKey(3);
                StartCoroutine(DestroyAfterDelay(3f));
                break;
            case 2003:
                //Destroy self after three seconds- text only appears once.
                if (playerController.m_key2Obtained == true || playerController.m_key1Obtained ==true || playerController.m_key3Obtained == true)
                {
                    m_InteractionText = "This is another part of that key.";
                }
                playerController.CollectKey(4);
                StartCoroutine(DestroyAfterDelay(3f));
                break;
            case 2004:
                //Set inital spawnpoint
                playerController.NewCheckpoint(new Vector2(0.57f, -0.31f));
                StartCoroutine(DestroyAfterDelay(1f));
                break;
            case 2005:
                if (playerController.m_key5Obtained)
                {
                    //Set spawnpoint after reaching all keys
                    playerController.NewCheckpoint(new Vector2(0.57f, -0.31f));
                    StartCoroutine(DestroyAfterDelay(1f));
                }
                break;
            case 2006:
                if (playerController.m_key1Obtained)
                {
                    //Set spawnpoint after getting first key
                    playerController.NewCheckpoint(new Vector2(-6.61f, 8.5f));
                    StartCoroutine(DestroyAfterDelay(1f));
                }
                break;
            case 2007:
                if (playerController.m_key2Obtained)
                {
                    //Set spawnpoint after reaching second
                    playerController.NewCheckpoint(new Vector2(15.51f, 2.9f));
                    StartCoroutine(DestroyAfterDelay(1f));
                }
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

    IEnumerator MoveTextTowardsPlayer()
    {
        float elapsedTime = 0f;
        float duration = 3f; // How long Text lasts

        while (elapsedTime < duration)
        {
            Vector2 playerPosition = playerController.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)playerPosition + new Vector3(m_textPosition.x, m_textPosition.y, 0f));
            interactionText.rectTransform.position = screenPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}