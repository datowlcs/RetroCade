using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DotController : MonoBehaviour
{
    // on collision trigger
    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "pacman") 
        {
            Destroy(gameObject);
            PacmanController.score++;
            PacmanController.dotsRemaining--;
            if (PacmanController.dotsRemaining == 0)
            {
                // next level (increase speed of ghosts)
                if (GhostController.speed < GhostController.speedCap)
                    GhostController.speed += 1f;

                SceneManager.LoadScene("Pacman");
            }
        }
    }
}
