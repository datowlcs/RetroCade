﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void OnCancelClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnYesClick()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

}