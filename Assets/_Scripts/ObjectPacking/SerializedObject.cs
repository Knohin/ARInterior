using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedObject :MonoBehaviour
{
    public SerializedData sd;
    private void Start()
    {
        sd = new SerializedData();
    }
    
    public void SetCurrentState() // 현재상태 불러오기
    {
        sd.mFurniture = gameObject.name;
        sd.mPosition = gameObject.transform.position;
        sd.mRotation = gameObject.transform.rotation;
        sd.mScale = gameObject.transform.lossyScale;

        if (gameObject.GetComponent<changeColor>() != null)
        {
            sd.mColor = gameObject.GetComponent<changeColor>().GetColor();
        }
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            sd.mesh = MeshSerializer.WriteMesh(gameObject.GetComponent<MeshFilter>().mesh, true);
        }
    }

    public void SetSdToObject() // load한 데이터를 오브젝트에 넣음
    {
        gameObject.name = sd.mFurniture;
        gameObject.transform.position = sd.mPosition;
        gameObject.transform.rotation = sd.mRotation;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localScale = new Vector3(
            sd.mScale.x / gameObject.transform.lossyScale.x,
            sd.mScale.y / gameObject.transform.lossyScale.y,
            sd.mScale.z / gameObject.transform.lossyScale.z);

        if (gameObject.GetComponent<changeColor>() != null)
        {
            gameObject.GetComponent<changeColor>().Start();
            gameObject.GetComponent<changeColor>().SetColor(sd.mColor);
        }
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            gameObject.GetComponent<MeshFilter>().mesh = MeshSerializer.ReadMesh(sd.mesh);
        }
    }
}

[System.Serializable]
public class SerializedData
{
    public string mFurniture;
    public Vector3 mPosition;
    public Quaternion mRotation;
    public Vector3 mScale;
    public int mColor;
    public byte[] mesh;
}