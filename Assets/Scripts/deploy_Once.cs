using System;
using UnityEngine;
using Vuforia;

public class deploy_Once : MonoBehaviour
{

    public GameObject AnchorStage;
    public GameObject MasterPlane;
    private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;
    
    public void Start()
    {
        if (AnchorStage == null)
        {
            Debug.Log("AnchorStage must be specified");
            return;
        }

        AnchorStage.SetActive(false);
        MasterPlane.SetActive(false);
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

            //MasterPlane.transform.parent = anchor.transform;
            MasterPlane.transform.localPosition = AnchorStage.transform.position;
            MasterPlane.transform.localRotation = AnchorStage.transform.localRotation;
            MasterPlane.SetActive(true);
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