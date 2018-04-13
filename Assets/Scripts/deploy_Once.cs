using System;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using myMath;

public class deploy_Once : MonoBehaviour
{
    public GameObject AnchorStage;
    public GameObject MasterPlane;
    private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;

    public GameObject image_target;
    public Camera mainCamera;
    public Text mytext;
    public Text mytext2;
    public Text mytext3;
    
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
            AnchorStage.transform.localScale = new Vector3(1, 1, 1);
            AnchorStage.SetActive(true);

            //MasterPlane.transform.parent = anchor.transform;
            MasterPlane.transform.localPosition = AnchorStage.transform.position;
            MasterPlane.transform.localRotation = AnchorStage.transform.localRotation;
            MasterPlane.transform.localScale = AnchorStage.transform.localScale;
            MasterPlane.SetActive(true);

            //거리 계산 시작
            //        dist_planeTocamera tempVar = new dist_planeTocamera();
            //       tempVar.init();
            Vector3 targetP = image_target.transform.position;
            Vector3 cameraP = mainCamera.transform.position;
            mytext.text = (common_math.getDistance(targetP, cameraP)).ToString() + "미터";
            // mytext.text = (targetP.x - cameraP.x).ToString();

            //mytext.text = imgtarget.GetSize().x.ToString();
            //mytext2.text = imgtarget.GetSize().y.ToString();

            Vector3 targetP2 = MasterPlane.transform.position;
            mytext2.text = (common_math.getDistance(targetP2, cameraP)).ToString() + "Cm";
            //mytext2.text = (targetP.y - cameraP.y).ToString();
            mytext3.text = (targetP.z - cameraP.z).ToString();

            //거리 계산 끝

            //거리 대입
            GetComponent<PlaneFinderBehaviour>().Height = common_math.getDistance(targetP, cameraP);

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