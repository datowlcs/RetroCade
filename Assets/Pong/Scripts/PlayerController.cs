using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float m_DeltaY;
    bool m_Touched = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        m_DeltaY = touchPos.y - transform.position.y;
                        m_Touched = true;
                    }
                    break;
                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && m_Touched)
                        transform.position += new Vector3(0, touchPos.y - m_DeltaY);
                    break;

                case TouchPhase.Ended:
                    m_Touched = false;
                    break;
            }

        }
    }

}
