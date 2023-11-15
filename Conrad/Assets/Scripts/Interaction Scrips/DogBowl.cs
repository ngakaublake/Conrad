using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBowl : MonoBehaviour
{
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public int currentSpriteIndex = 0;
    private bool AlreadyEaten;
    // Start is called before the first frame update
    void Start()
    {
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
        ProcessInteraction();
        ProcessEatOverTime();
        if (playerController.m_IsPlayerinCognitiveWorld)
        {
            AlreadyEaten = true;
        }
        else
        {
            AlreadyEaten = false;
        }
    }

    public void UpdateBowl(int _level)
    {
        if (_level == 3)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (_level == 2)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (_level == 1)
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
    
    void ProcessEatOverTime()
    {
        if (playerController.m_IsPlayerinCognitiveWorld)
        {
            UpdateBowl(2);
        }
        if (playerController.m_CognitiveWorldResetting)
        {
            UpdateBowl(1);
        }
    }

    void ProcessInteraction()
    {

        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, playerController.transform.position) < 1f)
        {
            //Fill it up
            UpdateBowl(3);
        }
    }
}
