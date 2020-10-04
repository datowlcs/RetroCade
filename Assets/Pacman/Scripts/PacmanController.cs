using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PacmanController : MonoBehaviour {
    
     // global vars
    public static int score = 0;
    public static int lives = 3;
    public static float speed = 0.25f;
    public static int dotsRemaining;

    // swipe vars
    public float SWIPE_THRESHOLD = 20f;
    private Vector2 destination = Vector2.zero;
    private Vector2 swipeStart;
    private Vector2 swipeEnd;
    
    // text display vars
    public Text scoreDisplay;
    public Text livesDisplay;


    void Start () {
        destination = transform.position;
        dotsRemaining = GameObject.FindGameObjectsWithTag("Pacdot").Length;
    }

    void FixedUpdate () {;

        scoreDisplay.text = score.ToString();
        livesDisplay.text = lives.ToString();

        // gradualy move towards destination
        Vector2 path = Vector2.MoveTowards(transform.position, destination, speed);
        GetComponent<Rigidbody2D>().MovePosition(path);


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
                destination = (Vector2)transform.position + (Vector2) new Vector2(0, 35);
                GetComponent<Animator>().SetBool("movingUp", true);
                GetComponent<Animator>().SetBool("movingDown", false);
                GetComponent<Animator>().SetBool("movingRight", false);
                GetComponent<Animator>().SetBool("movingLeft", false);
            }
            // downwards swipe
            else if ((swipeEnd.y - swipeStart.y < 0) && canMove(-Vector2.up))
            {
                destination = (Vector2)transform.position - (Vector2) new Vector2(0, 35);
                GetComponent<Animator>().SetBool("movingDown", true);
                GetComponent<Animator>().SetBool("movingUp", false);
                GetComponent<Animator>().SetBool("movingRight", false);
                GetComponent<Animator>().SetBool("movingLeft", false);
            }
            swipeStart = swipeEnd;
        }
        // horizontal swipes
        if (Mathf.Abs(swipeEnd.x - swipeStart.x) > SWIPE_THRESHOLD
            && Mathf.Abs(swipeEnd.x - swipeStart.x) > Mathf.Abs(swipeEnd.y - swipeStart.y))
        {
            // right swipe
            if ((swipeEnd.x - swipeStart.x > 0) && canMove(-Vector2.right))
            {
                destination = (Vector2)transform.position - (Vector2) new Vector2(35, 0);
                GetComponent<Animator>().SetBool("movingRight", true);
                GetComponent<Animator>().SetBool("movingUp", false);
                GetComponent<Animator>().SetBool("movingDown", false);
                GetComponent<Animator>().SetBool("movingLeft", false);
            }
            // left swipe
            else if ((swipeEnd.x - swipeStart.x < 0) && canMove(Vector2.right))
            {
                destination = (Vector2)transform.position + (Vector2) new Vector2(35, 0);
                GetComponent<Animator>().SetBool("movingLeft", true);
                GetComponent<Animator>().SetBool("movingUp", false);
                GetComponent<Animator>().SetBool("movingDown", false);
                GetComponent<Animator>().SetBool("movingRight", false);
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