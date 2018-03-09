using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToCamera : MonoBehaviour {

    private Camera TargetCamera;

    private void Awake()
    {
        TargetCamera = Camera.current;
    }

    void Update () {
        this.transform.rotation = TargetCamera.transform.rotation;
	}
}
