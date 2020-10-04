using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SWIPE_THRESHOLD = 20f;
    private Vector2 destination = Vector2.zero;
    private Vector2 swipeStart;
    private Vector2 swipeEnd;

    void Start()
    {
        destination = transform.position;
    }

    void FixedUpdate()
    {
        // checking user input - mobile
        if (((Vector2)transform.position == destination) || !(canMove((Vector2)transform.position)))
        {
            // obtaining first user input on screen
            foreach (Touch touch in Input.touches)
            {
                // converting touch from screen coordinates to unity coordinates
                // Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (touch.phase == TouchPhase.Began)
                {
                    swipeStart = touch.position;
                    swipeEnd = touch.position;
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    swipeEnd = touch.position;
                    performSwipeMovement();
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    swipeEnd = touch.position;
                    performSwipeMovement();
                }
            }
        }
    }

    void performSwipeMovement()
    {
        // vertical swipes
        if (Mathf.Abs(swipeEnd.y - swipeStart.y) > SWIPE_THRESHOLD
            && Mathf.Abs(swipeEnd.y - swipeStart.y) > Mathf.Abs(swipeEnd.x - swipeStart.x))
        {
            // upwards swipe
            if ((swipeEnd.y - swipeStart.y > 0) && canMove(Vector2.up))
            {
                destination = (Vector2)transform.position + (Vector2)new Vector2(0, 35);

            }
            // downwards swipe
            else if ((swipeEnd.y - swipeStart.y < 0) && canMove(-Vector2.up))
            {
                destination = (Vector2)transform.position - (Vector2)new Vector2(0, 35);

            }
            swipeStart = swipeEnd;
        }

    }

    bool canMove(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     float m_DeltaY;
//     bool m_Touched = false;

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);
//             Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

//             switch (touch.phase)
//             {
                
//                 case TouchPhase.Began:
//                     if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
//                     {
//                         m_DeltaY = touchPos.y - transform.position.y;
//                         m_Touched = true;
//                     }
//                     break;
//                 //Determine if the touch is a moving touch
//                 case TouchPhase.Moved:
//                     if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && m_Touched)
//                         transform.position += new Vector3(0, touchPos.y - m_DeltaY);
//                     break;

//                 case TouchPhase.Ended:
//                     m_Touched = false;
//                     break;
//             }

//         }
//     }

// }
