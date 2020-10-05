using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineController : MonoBehaviour
{
    [SerializeField]
    int m_SceneIndex;
    GameObject focusObj;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began)
        {
            focusObj = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            
            {
                focusObj=hit.transform.gameObject;
            }
        }
          
        if(focusObj && Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // SceneManager.LoadScene(m_SceneIndex, LoadSceneMode.Single);
            if (focusObj.tag == "Pong")
            {
                Time.timeScale = 1;
                 SceneManager.LoadScene("Pong");
            }
            else if (focusObj.tag == "Pacman")
            {
                Time.timeScale = 1;
                 SceneManager.LoadScene("Pacman");
            }
        
        
        }

        if(focusObj && Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            focusObj=null;
        }
    }
}
