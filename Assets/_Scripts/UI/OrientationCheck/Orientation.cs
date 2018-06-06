using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//using scene 00_Start
public class Orientation : MonoBehaviour
{
    public static event UnityAction<ScreenOrientation> orientationChangedEvent;
    public static ScreenOrientation _orientation;
    public GameObject InitialPanel;
    public GameObject portraitPanel;
    public GameObject landscapePanel;

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
        if (Input.deviceOrientation == DeviceOrientation.Portrait ||
            Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            InitialPanel.SetActive(false);
            portraitPanel.SetActive(true);
            landscapePanel.SetActive(false);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
        Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            InitialPanel.SetActive(false);
            portraitPanel.SetActive(false);
            landscapePanel.SetActive(true);
        }
        else if (Input.deviceOrientation == DeviceOrientation.FaceUp ||
        Input.deviceOrientation == DeviceOrientation.FaceDown)
        {
            if (_orientation == ScreenOrientation.Portrait ||
                _orientation == ScreenOrientation.PortraitUpsideDown)
            {
                InitialPanel.SetActive(false);
                portraitPanel.SetActive(true);
                landscapePanel.SetActive(false);
            }
            else if (_orientation == ScreenOrientation.LandscapeLeft ||
                _orientation == ScreenOrientation.LandscapeRight ||
                _orientation == ScreenOrientation.Landscape)
            {
                InitialPanel.SetActive(false);
                portraitPanel.SetActive(false);
                landscapePanel.SetActive(true);
            }
            else
            {
                _orientation = ScreenOrientation.Portrait;
                InitialPanel.SetActive(false);
                portraitPanel.SetActive(true);
                landscapePanel.SetActive(false);
            }
        }
        if (_orientation != Screen.orientation)
        {
            _orientation = Screen.orientation;
            OnOrientationChanged(_orientation);
        }
    }
}