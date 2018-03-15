/*
 * 설명 : 
 *      오브젝트에 이 스크립트를 추가하면,
 *      항상 이 오브젝트는 카메라가 바라보는 방향과 평행을 유지한다.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToCamera : MonoBehaviour {

    private Camera TargetCamera;

    private void Start()
    {
        TargetCamera = Camera.main;
    }

    void Update () {
        this.transform.rotation = TargetCamera.transform.rotation;
	}
}
