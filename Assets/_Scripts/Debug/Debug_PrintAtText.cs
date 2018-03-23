/*
 * 그냥 디버깅용으로 Text에 Log 찍어보려고 만듬
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_PrintAtText : MonoBehaviour {

    public GameObject ARCamera;
    public GameObject ImageTarget;

    private Text DebugText;

    private void Start()
    {
        DebugText = GetComponent<Text>();
        if (DebugText == null)
            Debug.LogError("DebugText(Text) is null !!");
    }

    void Update () {
        string str = "";
        str += "Position : " + ARCamera.transform.position.ToString();
        str += "\nRotation : " + ARCamera.transform.rotation.eulerAngles.ToString();
        str += "\nImageTarget Position : " + ImageTarget.transform.position.ToString();
        str += "\nImageTarget Height : " + (ARCamera.transform.position.y - ImageTarget.transform.position.y).ToString();
        DebugText.text = str;
	}
}
