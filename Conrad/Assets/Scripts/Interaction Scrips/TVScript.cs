using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVScript : MonoBehaviour
{
    public Sprite[] sprites;
    public int currentSpriteIndex = 0;
    public string m_Channel1Text;
    public string m_Channel2Text;
    public string m_Channel3Text;
    public string m_Channel4Text;
    public string m_Channel5Text;
    private bool m_switchchannel;
    private int m_channel;
    public int m_ID;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 m_textPosition;
    [SerializeField] private Text interactionText;

    // Start is called before the first frame update
    void Start()
    {
        HideInteractionText();
        m_channel = Random.Range(1, 6);
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (m_switchchannel)
        {
            m_channel = Random.Range(1, 6);
            m_switchchannel = false;
        }
        ProcessInteraction();
    }
    void ShowInteractionText(int _channel)
    {
        switch (_channel)
        {
            case 1:
                interactionText.text = m_Channel1Text;
                break;
            case 2:
                interactionText.text = m_Channel2Text;
                break;
            case 3:
                interactionText.text = m_Channel3Text;
                break;
            case 4:
                interactionText.text = m_Channel4Text;
                break;
            case 5:
                interactionText.text = m_Channel5Text;
                break;
            default:
                interactionText.text = m_Channel1Text;
                break;
        }
        interactionText.gameObject.SetActive(true); // Show
    }
    void HideInteractionText()
    {
        interactionText.gameObject.SetActive(false); //bro
    }

    void ProcessInteraction()
    {

        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, playerController.transform.position) < 1f)
        {
            StartCoroutine(MoveTextTowardsPlayer());
            ShowInteractionText(m_channel);
        }
    }

    void CustomEvent()
    {

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
        m_switchchannel = true;
    }
}
