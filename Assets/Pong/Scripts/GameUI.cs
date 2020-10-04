using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    Text m_PlayerScoreText;

    [SerializeField]
    Text m_OpponentScoreText;

    [SerializeField]
    Text m_Timer;

    [SerializeField]
    BallController Ball;

    [SerializeField]
    GameObject m_PauseUI;

    [SerializeField]
    GameObject m_GameOverUI;

    /*[SerializeField]
    float m_MultiplayerTime;*/

    float m_Time;
    float m_PlayerScore;
    float m_OpponentScore;

    [HideInInspector]
    public float Minutes;

    [HideInInspector]
    public float Seconds;

    // Start is called before the first frame update
    void Start()
    {
        m_Time = 0;
        m_PlayerScore = Ball.PlayerScore;
        m_OpponentScore = Ball.OpponentScore;
    }

    // Update is called once per frame
    void Update()
    {        
        Seconds = m_Time % 60;
        Minutes = (float) System.Math.Floor(m_Time / 60);
        m_Timer.text = string.Format("Time: {0:0}:{1:00}", Minutes, Seconds);

        /* For Multiplayer
            if (m_Time == 0) { GameOver();  }
            m_Time -= Time.deltaTime;
        */

        m_Time += Time.deltaTime;

        m_PlayerScore = Ball.PlayerScore;
        m_OpponentScore = Ball.OpponentScore;
        m_PlayerScoreText.text = m_PlayerScore.ToString();
        m_OpponentScoreText.text = m_OpponentScore.ToString();

        if (Ball.SinglePlayerGameOver)
        {
            GameOver();
        }
    }

    public void OnPauseClick()
    {
        m_PauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    void GameOver()
    {
        m_GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
}
