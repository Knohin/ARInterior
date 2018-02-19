using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedObject : MonoBehaviour {

    public Vector3 mPosition;
    public Quaternion mRotation;
    public Vector3 mScale;
    // ... and some

    private void Start()
    {
        SetCurrentState();
    }

    public void SetCurrentState()
    {
        mPosition = transform.position;
        mRotation = transform.rotation;
        mScale = transform.localScale;
}
}
