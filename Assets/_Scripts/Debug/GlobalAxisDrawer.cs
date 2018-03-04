using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGlobalAxis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        Debug.DrawLine(pos, pos + Vector3.right, Color.red);
        Debug.DrawLine(pos, pos + Vector3.up, Color.green);
        Debug.DrawLine(pos, pos + Vector3.forward, Color.blue);
        Debug.DrawLine(Vector3.zero, Vector3.right, Color.red);
        Debug.DrawLine(Vector3.zero, Vector3.up, Color.green);
        Debug.DrawLine(Vector3.zero, Vector3.forward, Color.blue);
    }
}
