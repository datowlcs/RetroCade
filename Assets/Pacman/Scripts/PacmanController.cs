using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour {
    public float speed = 0.4f;
    private Vector2 destination = Vector2.zero;
    private Vector2 swipeStart;
    private Vector2 swipeEnd;

    public float SWIPE_THRESHOLD = 20f;

    void Start () {
        destination = transform.position;
    }

    void FixedUpdate () {
        // gradualy move towards destination
        Vector2 path = Vector2.MoveTowards(transform.position, destination, speed);
        GetComponent<Rigidbody2D>().MovePosition(path);

        // checking user input - TESTING
        // if (((Vector2) transform.position == destination) || !(canMove((Vector2) transform.position)))
        // {
        //     if (Input.GetKey(KeyCode.UpArrow) && canMove(Vector2.up))
        //         destination = (Vector2)transform.position + (Vector2) new Vector2(0, 35);
        //     if (Input.GetKey(KeyCode.RightArrow) && canMove(-Vector2.right))
        //         destination = (Vector2)transform.position - (Vector2) new Vector2(35, 0);
        //     if (Input.GetKey(KeyCode.DownArrow) && canMove(-Vector2.up))
        //         destination = (Vector2)transform.position - (Vector2) new Vector2(0, 35);
        //     if (Input.GetKey(KeyCode.LeftArrow) && canMove(Vector2.right))
        //         destination = (Vector2)transform.position + (Vector2) new Vector2(35, 0);

        //     Vector2 dir = destination - (Vector2)transform.position;
        //     GetComponent<Animator>().SetFloat("XDirection", dir.x);
        //     GetComponent<Animator>().SetFloat("YDirection", dir.y);

        // }

        // checking user input - mobile
        if (((Vector2) transform.position == destination) || !(canMove((Vector2) transform.position)))
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

        // animations
        Vector2 dir = destination - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("XDirection", dir.x);
        GetComponent<Animator>().SetFloat("YDirection", dir.y);
    }

    void performSwipeMovement()
    {
        // vertical swipes
        if (Mathf.Abs(swipeEnd.y - swipeStart.y) > SWIPE_THRESHOLD
            && Mathf.Abs(swipeEnd.y - swipeStart.y) > Mathf.Abs(swipeEnd.x - swipeStart.x))
        {
            // upwards swipe
            if (swipeEnd.y - swipeStart.y > 0)
            {
                destination = (Vector2)transform.position + (Vector2) new Vector2(0, 35);
            }
            // downwards swipe
            else if (swipeEnd.y - swipeStart.y < 0)
            {
                destination = (Vector2)transform.position - (Vector2) new Vector2(0, 35);
            }
            swipeStart = swipeEnd;
        }
        // horizontal swipes
        if (Mathf.Abs(swipeEnd.x - swipeStart.x) > SWIPE_THRESHOLD
            && Mathf.Abs(swipeEnd.x - swipeStart.x) > Mathf.Abs(swipeEnd.y - swipeStart.y))
        {
            // right swipe
            if (swipeEnd.x - swipeStart.x > 0)
            {
                destination = (Vector2)transform.position - (Vector2) new Vector2(35, 0);
            }
            // left swipe
            else if (swipeEnd.x - swipeStart.x < 0)
            {
                destination = (Vector2)transform.position + (Vector2) new Vector2(35, 0);
            }
            swipeStart = swipeEnd;
        }
    }

    bool canMove (Vector2 dir) {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}