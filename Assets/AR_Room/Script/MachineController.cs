using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineController : MonoBehaviour
{
    [SerializeField]
    int m_SceneIndex;

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
                    // Record initial touch position.
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        SceneManager.LoadScene(m_SceneIndex, LoadSceneMode.Single);
                    }
                    break;
            }

        }
    }
}
