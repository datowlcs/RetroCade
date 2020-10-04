using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    float m_Speed;

    [SerializeField]
    float m_Acceleration;

    [SerializeField]
    AudioClip m_BounceSfx;

    [HideInInspector]
    public float PlayerScore;

    [HideInInspector]
    public float OpponentScore;

    [HideInInspector]
    public bool SinglePlayerGameOver = false;

    Vector2 m_Direction;
    float m_XPosition;
    float m_YPosition;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerScore = 0;
        OpponentScore = 0;
        m_Direction = Vector2.one.normalized;
        m_XPosition = transform.position.x;
        m_YPosition = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Speed += m_Acceleration;
        transform.Translate(Time.deltaTime * m_Speed * m_Direction);


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            m_Direction.y = -m_Direction.y;
        }
        else if (col.gameObject.tag == "Bouncer")
        {
            AudioSource.PlayClipAtPoint(m_BounceSfx, col.transform.position);
            m_Direction.x = -m_Direction.x;
        }
        else if (col.gameObject.tag == "SideBorder")
        {
            Reset(col.gameObject);
        }

    }

    private void Reset(GameObject col)
    {
        gameObject.transform.position = new Vector2(m_XPosition, m_YPosition);
        if (col.name == "LeftBorder")
        {
            SinglePlayerGameOver = true;
            OpponentScore += 1;
        }
        else if (col.name == "RightBorder")
        {
            PlayerScore += 1;
        }
    }
}
