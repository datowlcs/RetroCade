using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float m_DeltaY;
    Rigidbody2D m_Rb;
    bool m_Touched = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        m_DeltaY = touchPos.y - transform.position.y;
                        m_Touched = true;
                    }
                    break;
                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && m_Touched)
                        m_Rb.MovePosition(new Vector2(0, touchPos.y - m_DeltaY));
                    break;

                case TouchPhase.Ended:
                    m_Touched = false;
                    break;
            }

        }
    }

}
