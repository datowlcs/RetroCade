﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GhostController : MonoBehaviour
{

    // points for the ghosts to navigate towards
    public Transform[] targetPoints;
    int currPoint = 0;

    public float speed = 0.15f;

    void FixedUpdate()
    {

        // check if the ghost has reached the current target point yet
        if (transform.position != targetPoints[currPoint].position)
        {
            Vector2 point = Vector2.MoveTowards(transform.position, targetPoints[currPoint].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(point);
        }
        else 
        {
            currPoint = (currPoint + 1) % targetPoints.Length;
        }

        // animations
        Vector2 dir = targetPoints[currPoint].position - transform.position;
        GetComponent<Animator>().SetFloat("XDirection", dir.x);
        GetComponent<Animator>().SetFloat("YDirection", dir.y);
    }

    void OnTriggerEnter2D(Collider2D other) {
        // destroy pacman if they collide 
        if (other.name == "pacman") 
        {
            if (PacmanController.lives == 1)
            {
                PacmanController.score = 0;
                PacmanController.lives = 3;
                SceneManager.LoadScene("Pacman");
            }
            else 
            {
                PacmanController.lives--;
            }
        }
    }
}