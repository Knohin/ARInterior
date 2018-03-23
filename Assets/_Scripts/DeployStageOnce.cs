using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;

public class DeployStageOnce : MonoBehaviour
{
    public GameObject AnchorStage;
    private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;

    public GameObject ImageTarget;


    public void Start()
    {
        if(ImageTarget == null)
        {
            Debug.LogError("ImageTarget is null.");
        }
        if (AnchorStage == null)
        {
            Debug.LogError("AnchorStage must be specified.");
            return;
        }

        AnchorStage.SetActive(false);
    }

    public void Awake()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    public void OnDestroy()
    {
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnVuforiaStarted()
    {
        _deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
    }
    

    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (result == null || AnchorStage == null)
        {
            Debug.LogWarning("Hit test is invalid or AnchorStage not set");
            return;
        }

        var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);

        if (anchor != null)
        {
            AnchorStage.transform.parent = anchor.transform;
            AnchorStage.transform.localPosition = Vector3.zero;
            AnchorStage.transform.localRotation = Quaternion.identity;
            AnchorStage.SetActive(true);
        }

        if (_previousAnchor != null)
        {
            Destroy(_previousAnchor);
        }

        _previousAnchor = anchor;
    }

    public void OnAutomaticHitText(HitTestResult result)
    {
        if (result == null || AnchorStage == null)
        {
            Debug.LogWarning("Hit test is invalid or AnchorStage not set");
            return;
        }
        var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);

        if (anchor != null)
        {
            AnchorStage.transform.parent = anchor.transform;
            AnchorStage.transform.localPosition = Vector3.zero;
            AnchorStage.transform.localRotation = Quaternion.identity;
            AnchorStage.SetActive(true);
        }

        if (_previousAnchor != null)
        {
            Destroy(_previousAnchor);
        }

        _previousAnchor = anchor;
    }



    public void PerformHitTestToScreenCenter()
    {
        GetComponent<PlaneFinderBehaviour>().PerformHitTest(new Vector2(Screen.width / 2, Screen.height / 2));
    }
}
