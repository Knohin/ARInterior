using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public GameObject CenterMarker;
    public GameObject JengaPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate<GameObject>(JengaPrefab, CenterMarker.transform.position, Quaternion.identity);
                Debug.Log("Jenga created! on " + CenterMarker.transform.position);
            }
        }
        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Instantiate<GameObject>(JengaPrefab, CenterMarker.transform.position, Quaternion.identity);
                Debug.Log("Jenga created! on " + CenterMarker.transform.position);
            }
        }
	}
}
