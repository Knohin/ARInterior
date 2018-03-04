#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraController : MonoBehaviour {

    private Transform cameraTr;

    private void Start()
    {
        cameraTr = Camera.main.transform;
    }

    void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            float ver = Input.GetAxis("Mouse X");
            float hor = Input.GetAxis("Mouse Y");

            cameraTr.Rotate(new Vector3(0, ver*2, 0), Space.World);
            cameraTr.Rotate(new Vector3(-hor*2, 0, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraTr.Translate(new Vector3(-0.1f, 0, -0),Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraTr.Translate(new Vector3(0, 0, -0.1f), Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraTr.Translate(new Vector3(0.1f, 0, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.W))
        {
            cameraTr.Translate(new Vector3(0, 0, 0.1f), Space.Self);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            cameraTr.Translate(new Vector3(0, 0.1f, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            cameraTr.Translate(new Vector3(0, -0.1f, 0), Space.Self);
        }


    }
}
#endif // UNITY_EDITOR