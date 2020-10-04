using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerController : MonoBehaviour
{
    [SerializeField]
    GameObject m_Object;

    float m_XPosition;

    void Start()
    {
        m_XPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_XPosition, m_Object.transform.position.y);
    }
}
