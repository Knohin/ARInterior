using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedObject :MonoBehaviour
{
    public SerializedData sd;
    private void Start()
    {
        //SetCurrentState();
        sd = new SerializedData();
        SetCurrentState();
    }
    
    public void SetCurrentState() // 바뀔 수 있는 상태
    {
        sd.save(gameObject);
    }
}

[System.Serializable]
public class SerializedData
{
    // 가구 정보
    public string mFurniture;
    public Vector3 mPosition;
    public Quaternion mRotation;
    public Vector3 mScale;
    public int mColor;
    
    // 벽 정보


    public void save(GameObject go) // 바뀔 수 있는 상태
    {
        mFurniture = go.name;
        mPosition = go.transform.position;
        mRotation = go.transform.rotation;
        mScale = go.transform.localScale;
        //mColor = go.GetComponent<MeshRenderer>().material.color;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}