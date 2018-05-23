using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Orientation2 : MonoBehaviour
{

    public static event UnityAction<ScreenOrientation> orientationChangedEvent;
    private ScreenOrientation _orientation;
    public GameObject portraitCanvus;
    public GameObject landscapeCanvus;
    public Text DebugText, DebugText2;

    void Start()
    {
        _orientation = Orientation._orientation;
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
        }
        if (Input.deviceOrientation == DeviceOrientation.Portrait ||
            Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            portraitCanvus.SetActive(true);
            landscapeCanvus.SetActive(false);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
        Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            portraitCanvus.SetActive(false);
            landscapeCanvus.SetActive(true);
        }
    }

    void Update()
    {
        DebugText.GetComponent<Text>().text = Input.deviceOrientation.ToString();
        DebugText2.GetComponent<Text>().text = Input.deviceOrientation.ToString();
    }
}