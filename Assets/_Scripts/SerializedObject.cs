using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedObject :MonoBehaviour
{
    public SerializedData sd; // MonoBehaviour 은 Serializable되지 않는다
    private void Start()
    {
        sd = new SerializedData();
    }
    
    public void SetCurrentState() // 현재상태 불러오기
    {
        sd.save(gameObject);
    }

    public void SetSdToObject() // load한 데이터를 오브젝트에 넣음
    {
        gameObject.name = sd.mFurniture;
        gameObject.transform.position = sd.mPosition;
        gameObject.transform.rotation = sd.mRotation;
        gameObject.transform.localScale = sd.mScale;
        gameObject.GetComponent<changeColor>().Start();
        gameObject.GetComponent<changeColor>().SetColor(sd.mColor);           
        
        if (gameObject.GetComponent<MeshFilter>() != null)
        {
            gameObject.GetComponent<MeshFilter>().mesh = MeshSerializer.ReadMesh(sd.mesh);
        }
    }
}

[System.Serializable]
public class SerializedData
{
    // 가구 & 벽 정보
    public string mFurniture;
    public Vector3 mPosition;
    public Quaternion mRotation;
    public Vector3 mScale;
    public int mColor;
    public byte[] mesh;

    public void save(GameObject go) // save 할 상태
    {
        mFurniture = go.name;
        mPosition = go.transform.position;
        mRotation = go.transform.rotation;
        mScale = go.transform.localScale;
        mColor = go.GetComponent<changeColor>().GetColor();

        if(go.GetComponent<MeshFilter>() != null)
        {
            Mesh m = go.GetComponent<MeshFilter>().mesh;
            mesh = MeshSerializer.WriteMesh(m, true);
        }
    }
}