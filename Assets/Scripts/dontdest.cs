using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
