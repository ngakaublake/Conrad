using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedEvent4 : MonoBehaviour
{
    public int m_ID;
    public bool m_AutoShow;
    public bool m_isInRange;
    private bool b_triggered;
    private PlayerController playerController;
    public GameObject RealPlayer;
    public GameObject CognitivePlayer;

    [SerializeField] private Vector2 m_textPosition;
    [SerializeField] private Text interactionText;
    [SerializeField] private Text finalText;
    [SerializeField] private DragFriend Conrad;
    [SerializeField] private ConradHealScript HealthConrad;
    [SerializeField] private HealthUI VisibleHealth;
    [SerializeField] private DogBehaviour RealWorldDog;

    [SerializeField] private EnemySpawner Spawner;
    [SerializeField] private EnemySpawner Spawner2;
    [SerializeField] private EnemySpawner Spawner3;
    [SerializeField] private EnemySpawner Spawner4;
    [SerializeField] private EnemySpawner Spawner5;
    [SerializeField] private EnemySpawner Spawner6;

    //UI Elements to delete
    [SerializeField] private GameObject UI;



    //Conversations in Act II
    public string m_Line1;
    public string m_Line2;
    public string m_Line3;
    public string m_Line4;
    public string m_Blank;

    // Start is called before the first frame update
    void Start()
    {
        HideInteractionText();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        b_triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInteraction();
    }
    void ShowInteractionText()
    {
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
            playerController.StopMoving(14);
            StartCoroutine(Scripted1());
            b_triggered = true; //so we aren't locked here
        }
    }


    IEnumerator Scripted1()
    {
        //Final Scene
        HealthConrad.Stopscript();
        UI.SetActive(false);
        playerController.ScriptedEventSunrise();
        yield return new WaitForSeconds(1f);
        //Player says 'conrad, we made it out'
        StartCoroutine(PlayerSpeaks());
        yield return new WaitForSeconds(4f);
        //Player says 'conrad?'
        StartCoroutine(PlayerSpeaks2());
        yield return new WaitForSeconds(2f);
        //Fade to black, somehow
        yield return new WaitForSeconds(2f);
        //Words I'm sorry, buddy appear on screen
        StartCoroutine(ImsorryBuddy());
        yield return new WaitForSeconds(3f);
        playerController.b_IsInLastStand = false;
        playerController.ScriptedTeleport(true);
        StartCoroutine(DogSpeaks());
        yield return new WaitForSeconds(3f);
        StartCoroutine(ImsorryBuddy());
        yield return new WaitForSeconds(2f);
    }

    IEnumerator ImsorryBuddy()
    {
        finalText.text = m_Line3; // Set
        float elapsedTime = 0f;
        float duration = 3f; // How long Text lasts

        while (elapsedTime < duration)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            finalText.rectTransform.position = screenCenter;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        finalText.text = m_Blank;
    }
    IEnumerator PlayerSpeaks()
    {
        interactionText.text = m_Line1; // Set
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
        interactionText.text = m_Blank;
    }
    IEnumerator PlayerSpeaks2()
    {
        interactionText.text = m_Line2; // Set
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
        interactionText.text = m_Blank;
    }
    IEnumerator DogSpeaks()
    {
        interactionText.text = m_Line4; // Set
        float elapsedTime = 0f;
        float duration = 3f; // How long Text lasts

        while (elapsedTime < duration)
        {
            Vector2 playerPosition = RealWorldDog.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint((Vector3)playerPosition + new Vector3(m_textPosition.x, m_textPosition.y, 0f));
            interactionText.rectTransform.position = screenPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        interactionText.text = m_Blank;
    }
}
