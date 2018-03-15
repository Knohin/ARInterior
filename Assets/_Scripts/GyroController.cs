/*
 * 설명 :
 *      현재 기기의 Gyro센서값을 받아와서 카메라를 움직인다.
 *      
 * 사용법 :
 *      메인 카메라에 이 스크립트를 추가한다.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroController : MonoBehaviour {

    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    [System.NonSerialized] public Text TextForDebug;

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.SetSiblingIndex(1);
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            //rot = new Quaternion(0, 0, 1, 0); // Euler(0,0,180)
            transform.localRotation = new Quaternion(0, 0, 1, 0);
        }

        TextForDebug = GameObject.Find("GyroText").GetComponent<Text>(); // DEBUG
    }

    private void Update()
    {
        if (gyro == null)
            return;

        //transform.localRotation = gyro.attitude * rot;
        transform.Rotate(-gyro.rotationRateUnbiased);

        string txt = "Gyro Att : " + gyro.attitude.eulerAngles;
        txt += "\nGyro g" + gyro.gravity;
        txt += "\nGyro b" + gyro.rotationRate;
        txt += "\nGyro ub" + gyro.rotationRateUnbiased;
        txt += "\nGyro UAcc" + gyro.userAcceleration;
        TextForDebug.text = txt;
    }
}
