using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshair : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    public void ScaleCrosshair()
    {
        if (transform.localScale.x > 0.8f)
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        }
        
    }

    public void ResetScaleCrosshair()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }
}
