using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//using scene 01_SelectUI
public class Orientation2 : MonoBehaviour
{
    public static event UnityAction<ScreenOrientation> orientationChangedEvent;
    private ScreenOrientation _orientation;
    public GameObject portrait;
    public GameObject landscape;

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
            portrait.SetActive(true);
            landscape.SetActive(false);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
        Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            portrait.SetActive(false);
            landscape.SetActive(true);
        }
        else if (Input.deviceOrientation == DeviceOrientation.FaceUp ||
        Input.deviceOrientation == DeviceOrientation.FaceDown)
        {
            if (_orientation == ScreenOrientation.Portrait ||
                _orientation == ScreenOrientation.PortraitUpsideDown)
            {
                portrait.SetActive(true);
                landscape.SetActive(false);
            }
            else if (_orientation == ScreenOrientation.LandscapeLeft ||
                _orientation == ScreenOrientation.LandscapeRight ||
                _orientation == ScreenOrientation.Landscape)
            {
                portrait.SetActive(false);
                landscape.SetActive(true);
            }
            else
            {
                _orientation = ScreenOrientation.Portrait;
                portrait.SetActive(true);
                landscape.SetActive(false);
            }
        }
    }
}