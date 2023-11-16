using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    public Transform player;
    private bool b_bookingitdownthehallway;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !b_bookingitdownthehallway)
        {
            //Look at Player
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle+90);
        }
    }
    public void DogBooksItDownTheHallwayScriptedEvent()
    {
        //Run Down the Hall!!!
        b_bookingitdownthehallway = true;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        StartCoroutine(RuntoPosition1(transform, new Vector2(0f, -9.67f), 4.5f));
    }

    IEnumerator RuntoPosition1(Transform unitTransform, Vector2 targetPosition, float duration)
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
        float elapsedTime = 0.0f;
        Vector2 startPosition = unitTransform.position;
        while (elapsedTime < duration)
        {
            unitTransform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        //Move to second Location
        unitTransform.position = targetPosition;
       StartCoroutine(RuntoPosition2(transform, new Vector2(1f, 0f), 4f));
    }

    IEnumerator RuntoPosition2(Transform unitTransform, Vector2 targetPosition, float duration)
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);
        float elapsedTime = 0.0f;
        Vector2 startPosition = unitTransform.position;
        while (elapsedTime < duration)
        {
            unitTransform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //Move to second Location
        unitTransform.position = targetPosition;
        Destroy(gameObject);
    }
}