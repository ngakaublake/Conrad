using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    private Transform player;
    private bool b_bookingitdownthehallway;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        StartCoroutine(MoveUnitOverTime(transform, new Vector2(1f, 0f), 5f));
    }

    IEnumerator MoveUnitOverTime(Transform unitTransform, Vector2 targetPosition, float duration)
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
        //Stop at Position & disappear
        unitTransform.position = targetPosition;
        Destroy(gameObject);
    }
}