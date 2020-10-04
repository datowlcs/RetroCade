using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
    }

    public void OnSingleClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
