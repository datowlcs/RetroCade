using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerController : MonoBehaviour
{
    [SerializeField]
    GameObject m_Object;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Bouncer")
        {
            transform.position = new Vector3((float)-0.121, m_Object.transform.position.y);
        }
       
    }
}
