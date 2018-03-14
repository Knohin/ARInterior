using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imagetargeting : MonoBehaviour {


    GameObject arCamera;
    GameObject um;

    // Use this for initialization
    void Start () {
        arCamera = GameObject.Find("ARCamera");
        um = GameObject.Find("MasterPlane");
        um.transform.TransformPoint(arCamera.transform.localPosition);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (um)
        {
            um.transform.TransformPoint(arCamera.transform.localPosition);
            Debug.Log("um's position" + um.transform.position);
        }
    }
}
