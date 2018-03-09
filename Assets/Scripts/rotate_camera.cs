using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_camera : MonoBehaviour {

    //backup, current, next Position
    Vector3 backupPosition, curPosition, nextPosition;
    //backup prev Rotation's value
    Quaternion backupRotation;

    public Camera mainCa;

    // Use this for initialization
    void Start()
    {
        //initialize value
        curPosition = nextPosition = Vector3.zero;
        //backup position, rotation
        backupPosition = mainCa.transform.position;
        backupRotation = mainCa.transform.rotation;
    }

    //touch sensor
    void inputTouch(Vector3 touchPosition)
    {
        //backup currentPosition
        backupPosition = mainCa.transform.position;
        backupRotation = mainCa.transform.rotation;

        //input mouse position
        curPosition = touchPosition;
    }

    //drag sensor
    void inputMove(Vector2 touchPosition)
    {
        nextPosition = touchPosition;

        //
        mainCa.transform.RotateAround(mainCa.transform.position, Vector3.up, (curPosition.x - nextPosition.x) / 10);

        //up, down rotation
        float updownangle = (curPosition.y - nextPosition.y) / 10;

        //roation at x
        mainCa.transform.Rotate(Vector3.left, updownangle);

        //char's rotation
        //not imple

        //save curposition
        curPosition = nextPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //touch sensor
        int tCount = Input.touchCount;

        //why need to 'for_loop'
        for (int i = 0; i < tCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                inputTouch(Input.GetTouch(i).position);
            }
            else if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                inputMove(Input.GetTouch(i).position);
            }
            else if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            inputTouch(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            inputMove(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //setting up function
        }
    }
}
