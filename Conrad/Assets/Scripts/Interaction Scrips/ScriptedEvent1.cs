using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedEvent1 : MonoBehaviour
{
    public int m_ID;
    public bool m_AutoShow;
    public bool m_isInRange;
    private bool b_triggered;
    private PlayerController playerController;
    [SerializeField] private Vector2 m_textPosition;
    [SerializeField] private Text interactionText;
    [SerializeField] private DogBehaviour CogDog;
    [SerializeField] private DragFriend Conrad;


    //Conversations in Act II
    public string m_Line1;
    public string m_Line2;

    // Start is called before the first frame update
    void Start()
    {
        HideInteractionText();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        b_triggered = false;
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
        interactionText.text = m_Line1; // Set
        interactionText.gameObject.SetActive(true); // Show
    }
    void HideInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

    void ProcessInteraction()
    {
        if (Vector2.Distance(transform.position, playerController.transform.position) < 1f && b_triggered == false)
        {
            StartCoroutine(ConradSpeaks());
            ShowInteractionText();
            CustomEvent();
            b_triggered = true; //so we aren't locked here
        }
    }

    void CustomEvent()
    {
        //Scripted Events
        playerController.StopMoving(5);
        if (CogDog != null)
        {
            CogDog.DogBooksItDownTheHallwayScriptedEvent();
        }
        if (Conrad != null)
        {
            Conrad.TriggerConradRunEvent();
        }


        GameObject fuseDoorYellow = GameObject.Find("FuseDoorYellow#4");
        if (fuseDoorYellow != null)
        {
            FuseDoor fuseDoorScript = fuseDoorYellow.GetComponent<FuseDoor>();
            if (fuseDoorScript != null)
            {
                fuseDoorScript.Scripted1CloseDoor();
            }
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideInteractionText();
        Destroy(gameObject);
    }

    IEnumerator ConradSpeaks()
    {
        float elapsedTime = 0f;
        float duration = 3f; // How long Text lasts

        while (elapsedTime < duration)
        {
            Vector2 ConradPosition = Conrad.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)ConradPosition + new Vector3(m_textPosition.x, m_textPosition.y, 0f));
            interactionText.rectTransform.position = screenPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}