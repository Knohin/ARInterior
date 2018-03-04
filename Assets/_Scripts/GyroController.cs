using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroController : MonoBehaviour {

    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    public Text TextForDebug;

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0); // Euler(0,0,180)
        }

        TextForDebug = GameObject.Find("GyroText").GetComponent<Text>(); // DEBUG
    }

    private void Update()
    {
        if (gyro == null)
            return;

        transform.localRotation = gyro.attitude * rot;

        string txt = "Gyro Att : " + gyro.attitude.eulerAngles;
        txt += "\nGyro g" + gyro.gravity;
        txt += "\nGyro b" + gyro.rotationRate;
        txt += "\nGyro ub" + gyro.rotationRateUnbiased;
        txt += "\nGyro UAcc" + gyro.userAcceleration;
        TextForDebug.text = txt;
    }
}
