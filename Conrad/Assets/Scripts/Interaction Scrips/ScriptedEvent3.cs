using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedEvent3 : MonoBehaviour
{
    public int m_ID;
    public bool m_AutoShow;
    public bool m_isInRange;
    private bool b_triggered;
    private PlayerController playerController;
    [SerializeField] private Vector2 m_textPosition;
    [SerializeField] private Text interactionText;
    [SerializeField] private DragFriend Conrad;
    [SerializeField] private ConradHealScript HealthConrad;
    [SerializeField] private HealthUI VisibleHealth;

    [SerializeField] private EnemySpawner Spawner;
    [SerializeField] private EnemySpawner Spawner2;
    [SerializeField] private EnemySpawner Spawner3;
    [SerializeField] private EnemySpawner Spawner4;
    [SerializeField] private EnemySpawner Spawner5;
    [SerializeField] private EnemySpawner Spawner6;


    //Conversations in Act II
    public string m_Line1;
    public string m_Line2;
    public string m_Line3;
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
            playerController.StopMoving(9);
            StartCoroutine(Scripted1());
            b_triggered = true; //so we aren't locked here
        }
    }


    IEnumerator Scripted1()
    {
        StartCoroutine(ConradSpeaks());
        ShowInteractionText();
        yield return new WaitForSeconds(5f);
        StartCoroutine(ConradSpeaks2());
        yield return new WaitForSeconds(30f);
        StartCoroutine(PlayerSpeaks());
        HealthConrad.StubToe();
        VisibleHealth.ShowConradHealth();
        Spawner.ManualTurnOn();
        Spawner2.ManualTurnOn();
        Spawner3.ManualTurnOn();
        Spawner4.ManualTurnOn();
        Spawner5.ManualTurnOn();
        Spawner6.ManualTurnOn();
    }

    IEnumerator ConradSpeaks()
    {
        interactionText.text = m_Line1; // Set
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
        interactionText.text = m_Blank;
    }
    IEnumerator ConradSpeaks2()
    {
        interactionText.text = m_Line2; // Set
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
        interactionText.text = m_Blank;
    }
    IEnumerator PlayerSpeaks()
    {
        interactionText.text = m_Line3; // Set
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
}
