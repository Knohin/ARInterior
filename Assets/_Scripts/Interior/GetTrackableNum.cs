using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GetTrackableNum : MonoBehaviour {

    StateManager stateManager;

    private void Start()
    {
        stateManager = TrackerManager.Instance.GetStateManager();
    }

    public void PrintLog()
    {
        var trackable = stateManager.GetActiveTrackableBehaviours();

        foreach(var t in trackable)
        {
            Debug.Log(t.name + " is Tracked!!!");
        }
    }
}
