using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changetext : MonoBehaviour {
    public Text ObjText;

	// Use this for initialization
	void Start () {
        ObjText = GameObject.Find("testtext").GetComponent<Text>();
        ObjText.text = "First_Text";	
	}
	

	// Update is called once per frame
	void Update () {
		
	}

    public void Change(float data)
    {
        Debug.Log("execute change");
        ObjText.text = data.ToString();
    }
}
