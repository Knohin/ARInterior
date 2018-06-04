using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnActiveOnTracked : MonoBehaviour {

    public Vuforia.ImageTargetBehaviour baseImage;
    private UnityEngine.UI.Button thisButton;


    void Start () {
        if (baseImage == null) Debug.LogError("baseImage is null");
        thisButton = GetComponent<UnityEngine.UI.Button>();
	}
	
	void Update ()
    {
        if (baseImage.CurrentStatus == Vuforia.TrackableBehaviour.Status.NOT_FOUND)
        {
            thisButton.interactable = false;
        }
        else if (baseImage.CurrentStatus == Vuforia.TrackableBehaviour.Status.TRACKED)
        {
            thisButton.interactable = true;
        }
    }
}
