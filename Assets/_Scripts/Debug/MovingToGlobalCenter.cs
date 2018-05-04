using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToGlobalCenter : MonoBehaviour {

    public Transform StartPoint;
    public float LoopTime;

    private float curTime;

	void Start () {
        if (LoopTime < 1.0f) LoopTime = 1.0f;

        curTime = 0.0f;
	}
	
	void Update () {
        curTime += Time.deltaTime;
        if (curTime > LoopTime)
            curTime -= LoopTime;
        transform.position =  Vector3.Lerp(StartPoint.position, Vector3.zero, curTime / LoopTime);
	}
}
