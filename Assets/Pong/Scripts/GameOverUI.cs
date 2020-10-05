using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    Text m_HighScore;

    [SerializeField]
    GameUI m_GameUI;

    void Start()
    {
        float m_Minutes = m_GameUI.Minutes;
        float m_Seconds = m_GameUI.Seconds;

        m_HighScore.text = string.Format("High Score: {0:0}:{1:00}", m_Minutes, m_Seconds);
    }

    public void OnExitClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
