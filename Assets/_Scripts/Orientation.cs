using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Orientation : MonoBehaviour {

    public static event UnityAction<ScreenOrientation> orientationChangedEvent;
    private ScreenOrientation _orientation;
    public Text myText;

    void Start()
    {
        _orientation = Screen.orientation;
        InvokeRepeating("CheckForChange", 1, .1f);
    }

    private static void OnOrientationChanged(ScreenOrientation orientation)
    {
        if (orientationChangedEvent != null)
            orientationChangedEvent(orientation);
    }

    private void CheckForChange()
    {        
        if (_orientation != Screen.orientation)
        {
            _orientation = Screen.orientation;
            OnOrientationChanged(_orientation);

            if (Input.deviceOrientation == DeviceOrientation.Portrait ||
            Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                
            }
            else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
            Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                
            }
        }
    }

    void Update()
    {
        myText.GetComponent<Text>().text = Input.deviceOrientation.ToString();
    }
}

