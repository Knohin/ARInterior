using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;
using UnityEngine.Events;

public class DeployStageOnce : MonoBehaviour
{
    public GameObject AnchorStage;
    private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;

    public GameObject ImageTarget;
    private Vuforia.PlaneFinderBehaviour PlaneFinder;


    public void Start()
    {
        PlaneFinder = GetComponent<PlaneFinderBehaviour>();
        if (PlaneFinder == null)
        {
            Debug.LogError("PlaneFinder is missing");
        }
        if (ImageTarget == null)
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

    public void Update()
    {
        //if (AnchorStage != null)
        //    AnchorStage.transform.Rotate(Vector3.up * Time.deltaTime * 30.0f);

        PlaneFinder.Height = (Camera.main.transform.position.y - ImageTarget.transform.position.y);
    }

    private void OnVuforiaStarted()
    {
        _deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
    }


    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (result == null)
        {
            Debug.LogWarning("Hit test is invalid");
            return;
        }
        if (AnchorStage == null)
        {
            Debug.LogWarning("AnchorStage not set");
            return;
        }

        var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);

        if (anchor != null)
        {
            AnchorStage.transform.parent = anchor.transform;
            AnchorStage.transform.localPosition = Vector3.zero;
            AnchorStage.transform.localRotation = Quaternion.identity;
            AnchorStage.SetActive(true);
            //anchor.transform.position = ImageTarget.transform.position;
            //anchor.transform.rotation = ImageTarget.transform.rotation;
        }

        if (_previousAnchor != null)
        {
            Destroy(_previousAnchor);
        }

        _previousAnchor = anchor;
        Debug.Log("-------------------------------Hit Test Works");

        // 마커 회전에 맞게 회전시키기
        // AnchorStage or _previousAnchor
        //if (AnchorStage != null)
        //    AnchorStage.transform.rotation = Camera.main.transform.rotation;

        //if (AnchorStage != null)
        //{
        //    Vector3 tempRotation = AnchorStage.transform.rotation.eulerAngles;
        //    tempRotation.y = Camera.main.transform.rotation.eulerAngles.y;

        //    AnchorStage.transform.rotation = Quaternion.Euler(tempRotation);
        //}

        if (AnchorStage != null)
        {
            //float angle = Quaternion.Angle(AnchorStage.transform.rotation, Camera.main.transform.rotation);
            AnchorStage.transform.rotation = ImageTarget.transform.rotation;
        }
    }

    public void PerformHitTestToScreenCenter()
    {
        PlaneFinder.PerformHitTest(new Vector2(Screen.width / 2, Screen.height / 2));
    }
}
