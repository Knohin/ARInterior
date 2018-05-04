/*
 * 그냥 디버깅용으로 Text에 Log 찍어보려고 만듬
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Debug_PrintAtText : MonoBehaviour {

    public GameObject ARCamera;
    public GameObject MainTarget;
    public Vuforia.PlaneFinderBehaviour PlaneFinder;
    public AnchorStageBehaviour anchorStageBehaviour;
    public GameObject anchorStage;

    private Text DebugText;
    private ImageTargetBehaviour mainTarget;

    private void Start()
    {
        DebugText = GetComponent<Text>();
        if (DebugText == null)
            Debug.LogError("DebugText(Text) is null !!");
        mainTarget = MainTarget.GetComponent<ImageTargetBehaviour>();
    }

    void Update () {
        string str = "";
        str += "==CAMERA==\nPosition : " + ARCamera.transform.position.ToString("N3");
        str += "\nRotation : " + ARCamera.transform.rotation.eulerAngles.ToString();

        str += "\n==ImageTarget==\nPosition : " + MainTarget.transform.position.ToString("N3");
        str += "\nRotation : " + MainTarget.transform.rotation.eulerAngles.ToString();
        str += "\nHeight : " + (ARCamera.transform.position.y - MainTarget.transform.position.y).ToString("N3");
        str += "\nState : " + mainTarget.CurrentStatus;
        str += "\n==Config==\nWorldCenterMode : " + VuforiaManager.Instance.WorldCenterMode;
        str += "\n==AnchorStage==\nPosition : " + anchorStage.transform.position.ToString("N3");
        str += "\nRotation : " + anchorStage.transform.rotation.eulerAngles.ToString();
        //str += "\nWorldCenterNmae : " + VuforiaManager.Instance.WorldCenter.Trackable.Name;

        //str += "\nCentralAnchorPoint : \n" + Vuforia.VuforiaManager.Instance.CentralAnchorPoint.position.ToString("N3"); // ?error except device tracking

        //str += "\nPFB Height : " + PlaneFinder.Height;
        //str += "\nPosePrediciton : " + Vuforia.VuforiaConfiguration.Instance.DeviceTracker.PosePrediction; // https://library.vuforia.com/content/vuforia-library/en/reference/unity/classVuforia_1_1VuforiaConfiguration_1_1DeviceTrackerConfiguration.html#aa4f9ed81d74e6932fa6760a77603eb7e

        //VuforiaManager.Instance.ARCameraTransform.Rotate(Vector3.up * Time.deltaTime * 30.0f, Space.World);
        //ARCamera.transform.Rotate(Vector3.up * Time.deltaTime * 30f, Space.World);
        DebugText.text = str;
	}
}
