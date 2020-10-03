using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    // on collision trigger
    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "pacman")
            Destroy(gameObject);
    }
}
