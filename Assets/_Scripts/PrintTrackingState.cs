using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PrintTrackingState : MonoBehaviour {

    public TrackableBehaviour mTrackableBehaviour;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
 //           mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
}
