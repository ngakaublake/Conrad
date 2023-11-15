using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedEvent2 : MonoBehaviour
{
    public int m_ID;
    public bool m_AutoShow;
    public bool m_isInRange;
    private bool b_triggered;
    private PlayerController playerController;
    [SerializeField] private CognitivePlayer cognitivePlayer;
    [SerializeField] private Vector2 m_textPosition;
    [SerializeField] private Text interactionText;
    [SerializeField] private EnemySpawner Spawner;
    [SerializeField] private EnemySpawner Spawner2;
    [SerializeField] private EnemySpawner Spawner3;
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
        if (Vector2.Distance(transform.position, playerController.transform.position) < 1f && b_triggered == false && playerController.b_Keydoorisopen)
        {
            StartCoroutine(Scripted1());
            Debug.Log("PLEASE");
            b_triggered = true; //so we aren't locked here
        }
        if (b_triggered)
        {
            cognitivePlayer.m_NoWarpCooldown = 3.0f;
        }
    }


    IEnumerator Scripted1()
    {
        Spawner.ManualTurnOn();
        Spawner2.ManualTurnOn();
        Spawner3.ManualTurnOn();
        StartCoroutine(ConradSpeaks());
        ShowInteractionText();
        yield return new WaitForSeconds(5f);
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
