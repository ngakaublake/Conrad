using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DragMode
{
    Mode_Drag,
    Mode_Drop
}

public class DragFriend : MonoBehaviour
{

    private bool isInRange; //Checks if player is in range of pickup
    public DragMode DragMode;
    public CognitivePlayer Player;
    public PlayerController PlayerController;
    [SerializeField] private PlayerShooting PlayerWeapons;
    Vector3 DragOffset = new Vector3(0.0f, 0.2f, 0.0f);
    public int RandomDrop;
    public bool b_dragEnabled;

    public float m_TimeBetweenDrag = 0.5f;
    public float m_CurrentTimeDrag = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        DragMode = DragMode.Mode_Drag;
        m_CurrentTimeDrag = 0f;
        isInRange = false;
        b_dragEnabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerReach")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerReach")
        {
            isInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (b_dragEnabled)
        {

            m_CurrentTimeDrag -= Time.deltaTime;
          

            if (isInRange == true && Input.GetKey(KeyCode.E)) //Checking if the Player is in range of the Pickup & Checking for Player Input 
            {
                if (RandomDrop != 1 && m_CurrentTimeDrag <= 0)
                {
                    PlayerWeapons.isCombatActive = false;
                    switch (DragMode) //Checking Item Type 
                    {
                        case DragMode.Mode_Drag:
                            transform.position = Player.transform.position - DragOffset;
                            //PlayerController.m_HorizontalVelocity = 0.5f;
                            //PlayerController.m_VerticalVelocity = 0.5f;
                            break;
                        case DragMode.Mode_Drop:

                            break;
                    }
                }
            }
            else
            {
                PlayerWeapons.isCombatActive = true;
            }

            RandomDrop = Random.Range(1000, 0);

            if (RandomDrop == 1)
            {
                //DragMode = DragMode.Mode_Drop;
                m_CurrentTimeDrag = m_TimeBetweenDrag;
            }

        }
    }


    //-- SCRIPTED EVENT 1 --//
    public void TriggerConradRunEvent()
    {
        StartCoroutine(WaitAndStartMoving(1f));
    }

    IEnumerator WaitAndStartMoving(float delay)
    {
        yield return new WaitForSeconds(delay);

        //Follows Dog
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
        //Stop at Position, then teleport to next position CODE IN LATER
        unitTransform.position = targetPosition;
        b_dragEnabled = true;
    }

}