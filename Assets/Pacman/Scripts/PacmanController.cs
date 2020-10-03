using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour {
    public float speed = 0.4f;
    Vector2 destination = Vector2.zero;

    void Start () {
        destination = transform.position;
    }

    void FixedUpdate () {
        // gradualy move towards destination
        Vector2 path = Vector2.MoveTowards(transform.position, destination, speed);
        GetComponent<Rigidbody2D>().MovePosition(path);

        // checking user input
        if ((Vector2) transform.position == destination) {
            if (Input.GetKey(KeyCode.UpArrow) && canMove(Vector2.up))
                destination = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && canMove(-Vector2.right))
                destination = (Vector2)transform.position - Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && canMove(-Vector2.up))
                destination = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && canMove(Vector2.right))
                destination = (Vector2)transform.position + Vector2.right;
        }

        // TODO: Fix this weird animation error
        // animations
        // Vector2 dir = destination - (Vector2)transform.position;
        // GetComponent<Animator>().setFloat("XDirection", dir.x);
        // GetComponent<Animator>().setFloat("YDirection", dir.y);
    }

    bool canMove (Vector2 dir) {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}