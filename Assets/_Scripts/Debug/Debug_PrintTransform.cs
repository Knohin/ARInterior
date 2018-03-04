using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_PrintTransform : MonoBehaviour {

    public Text DebugText;

    private void Start()
    {
        if (DebugText == null)
            Debug.LogError("DebugText(Text) is null !!");
    }

    void Update () {
        string str = "Position : " + transform.position.ToString();
        str += "\nRotation : " + transform.rotation.eulerAngles.ToString();
        DebugText.text = str;
	}
}
