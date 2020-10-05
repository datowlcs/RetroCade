using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    //Controllers 
    public Transform cameraTransform;
    public CharacterController characterController;

    //Movement settings
    public float cameraSensitivity;
    public float moveSpeed;
    public float moveInputDeadZone;

    //Touch Detection
    int leftFingerId, rightfingerId;
    float halfScreenWidth;

    //Camera Control
    Vector2 lookInput;
    float cameraPitch;

    //Movement speed
    Vector2 moveTouchStartPosition;
    Vector2 moveInput;


    void Start()
    {
        leftFingerId  = -1;
        rightfingerId = -1;

        halfScreenWidth = Screen.width / 2;

        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);

    }

    void Update()
    {
        for(int i = 0; i<Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch(t.phase)
            {
                case TouchPhase.Began:
                    if(t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        leftFingerId = t.fingerId;
                        Debug.Log("Tracking left finger");

                        moveTouchStartPosition = t.position;
                    }
                    else if(t.position.x > halfScreenWidth && rightfingerId == -1)
                    {
                        rightfingerId = t.fingerId;
                        Debug.Log("Tracking right finger");
                    }
                break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if(t.fingerId == rightfingerId)
                    {
                        rightfingerId = -1;
                        Debug.Log("Stopped tracking right finger");
                    }
                break;
                case TouchPhase.Moved:
                    if(t.fingerId == rightfingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if(t.fingerId == leftFingerId)
                    {
                        moveInput = t.position - moveTouchStartPosition;
                    }
                break;
                case TouchPhase.Stationary:
                    if(t.fingerId == rightfingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                break;
            }
        }

        if(rightfingerId != -1)
        {
            LookAround();
        }

        if(leftFingerId != -1)
        {
            Move();
        }
    }

    void LookAround()
    {
        //vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        //Horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;

        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }
}
