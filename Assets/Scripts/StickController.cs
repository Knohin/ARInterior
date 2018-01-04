using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public GameObject CameraObject;
    public GameObject StickObject;
    public float MovingSpeed;

    private Transform mCameraTr;
    private Transform mStickTr;
    private Vector3 mStickDistance = new Vector3(0.204f, -0.121f, 0.975f);

    // Use this for initialization
    void Start()
    {
        mCameraTr = CameraObject.transform;
        mStickTr = StickObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        mStickTr.position = Vector3.MoveTowards(
            mStickTr.position,
            mCameraTr.position,
            MovingSpeed * Time.deltaTime );
        mStickTr.rotation = mCameraTr.rotation;

        //Debug.Log(mStickTr.position - mCameraTr.position);
        Debug.Log(mCameraTr.position);
    }
}
